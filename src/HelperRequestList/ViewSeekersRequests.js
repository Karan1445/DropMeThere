import { toast, ToastContainer } from "react-toastify";
import CheckAcces from "../Auth-Jwt/CheckAcces";
import RequestshowCard from "./RequestShowCard";
import { useState, useEffect } from "react";
import axios from "axios";
import DisplayErros from "../Toast-Erro-Display/DisplayErrors";
import NavBar from "../NavBar/NavBar";
import { useNavigate } from "react-router-dom";

function ViewSeekerRequest() {
  const navigate = useNavigate();
  if (localStorage.getItem("IsDriver") === "No") {
    navigate("/Home");
  }
  const [users, setUsers] = useState([]);
  const [filteredUsers, setFilteredUsers] = useState([]);
  const [search, setSearch] = useState({ start: "", end: "", distance: "" });

  useEffect(() => {
    CheckAcces();
    fetchData();
  }, []);

  async function fetchData() {
    try {
      const token = localStorage.getItem("jwtToken");
      const response = await axios.get("http://localhost:5036/temporary", {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      const data = response.data;
      setUsers(data);
      setFilteredUsers(data);
    } catch (error) {
      DisplayErros(error);
      console.error("Error fetching data:", error);
    }
  }

  useEffect(() => {
    let filtered = users.filter((user) =>
      user.startPointName.toLowerCase().includes(search.start.toLowerCase()) &&
      user.endPointName.toLowerCase().includes(search.end.toLowerCase()) &&
      (search.distance === "" || parseFloat(user.distance) <= parseFloat(search.distance))
    );
    setFilteredUsers(filtered);
  }, [search, users]);

  async function doRequestConfirmation(data) {
    if (window.confirm("Did you want to help him?")) {
      var origin = localStorage.getItem("CurrentLocationLatLong");
      var destination = data.rideStartPointLatLong;
      const params = {
        origin,
        destination,
        mode: "driving",
        alternatives: false,
        steps: true,
        overview: "full",
        language: "en",
        traffic_metadata: false,
        api_key: "zfREBrvVLEZgcmMDr5SXwI9qYyvTSrJymkqiZICy",
      };
      try {
        const RouteResponse = await axios.post("https://api.olamaps.io/routing/v1/directions", null, { params });
        const objectForPost = {
          seekerUserID: data.userID,
          helperUserID: localStorage.getItem("UserID"),
          requestID: data.requestID,
          helpersCurrentLocationLatLong: origin,
          confirmationTime: new Date().toISOString(),
          helperRechabletimetoStartPoint: RouteResponse.data.routes[0].legs[0].readable_duration,
          helperDistanceFromStartPoint: RouteResponse.data.routes[0].legs[0].readable_distance,
        };
        const reply = await axios.post("http://localhost:5036/api/HelperSideViewRides", objectForPost, {
          headers: {
            Authorization: `Bearer ${localStorage.getItem("jwtToken")}`,
          },
        });
        toast.success("Help Assigned!");
        console.log(reply);
      } catch (error) {
        DisplayErros(error);
        console.log(error);
      }
    }
  }

  return (
    <>
      <NavBar />
      <div className="h-screen flex flex-col justify-start text-white py-12 px-12 sm:px-6 lg:px-8">
        <div className="max-w-7xl mx-auto">
          <h1 className="text-4xl font-bold mb-8 mt-8 text-center">Help Someone!</h1>
          <div className="mb-6 flex flex-col sm:flex-row gap-4 items-center justify-center">
            <input
              type="text"
              placeholder="Start Point"
              className="p-2 text-black rounded w-full sm:w-auto"
              value={search.start}
              onChange={(e) => setSearch({ ...search, start: e.target.value })}
            />
            <input
              type="text"
              placeholder="End Point"
              className="p-2 text-black rounded w-full sm:w-auto"
              value={search.end}
              onChange={(e) => setSearch({ ...search, end: e.target.value })}
            />
            <select
              className="p-2 text-black rounded w-full sm:w-auto"
              value={search.distance}
              onChange={(e) => setSearch({ ...search, distance: e.target.value })}
            >
              <option value="">Select Distance</option>
              <option value="1">1 km</option>
              <option value="5">5 km</option>
              <option value="10">10 km</option>
              <option value="15">15+ km</option>
            </select>
          </div>
          <div className="grid gap-5 grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4">
            {filteredUsers.length > 0 ? (
              filteredUsers.map((user) => (
                <RequestshowCard key={user.requestID} user={user} handleOnClick={doRequestConfirmation} />
              ))
            ) : (
              <div className="text-center text-gray-400 col-span-full text-xl">No requests found</div>
            )}
          </div>
        </div>
      </div>
      <ToastContainer />
    </>
  );
}

export default ViewSeekerRequest;
