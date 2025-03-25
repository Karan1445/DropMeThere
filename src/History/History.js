import React, { useState, useEffect } from "react";
import axios from "axios"; // Import axios
import { Gift, X } from "lucide-react"; // Import Lucide icons
import NavBar from "../NavBar/NavBar";

const History = () => {
  const [rideHistory, setRideHistory] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [showModal, setShowModal] = useState(false); // State to control modal visibility
  const [category, setCategory] = useState(""); // Category field value
  const [help, setHelp] = useState(""); // Help field value
  const [selectedRide, setSelectedRide] = useState(null); // Selected ride for gifting

  const isDriver = localStorage.getItem("IsDriver") === "Yes";
  const userID = localStorage.getItem("UserID"); // Fetch UserID from localStorage

  // Fetch ride history data from API using Axios
  const getRideHistory = async (userID) => {
    try {
      const response = await axios.get(`http://localhost:5036/api/History?UserID=${userID}`);
      console.log(response);
      setRideHistory(response.data);
      setLoading(false);
    } catch (err) {
      setError("Failed to fetch ride history");
      setLoading(false);
    }
  };

  // UseEffect hook to fetch ride history when component mounts
  useEffect(() => {
    if (userID) {
      getRideHistory(userID); // Fetch ride history with the UserID from localStorage
    }
  }, [userID]);

  const openModal = (ride) => {
    setSelectedRide(ride); // Set the selected ride for gifting
    setShowModal(true); // Show the modal
  };

  const closeModal = () => {
    setShowModal(false); // Close the modal
    setCategory(""); // Reset the fields
    setHelp(""); // Reset the fields
  };

  const handleGift = async () => {
    if (!category || !help) {
      alert("Please fill in both fields.");
      return;
    }

    try {
      const response = await axios.put(
        `http://localhost:5036/api/History?id=${selectedRide.historyId}&rrfd=${category}&HelpFromSeeker=${help}`,
        { category, help }
      );
      console.log("Gift sent successfully:", response.data);
      closeModal(); // Close modal after gift submission
    } catch (err) {
      alert("Failed to send gift.");
      console.error(err);
    }
  };

  const formatDate = (dateString) => {
    const date = new Date(dateString);
    const formattedDate = date.toLocaleDateString("en-US", {
      weekday: "long",
      year: "numeric",
      month: "long",
      day: "numeric",
    });
    const formattedTime = date.toLocaleTimeString("en-US", {
      hour: "2-digit",
      minute: "2-digit",
      hour12: true,
    });
    return `on ${formattedDate} at ${formattedTime}`;
  };

  // Formatting for ride time and request time to be displayed correctly
  const formatTimeDifference = (rideStart, reqTime) => {
    const timeToGetRide = Math.abs(Math.round((rideStart - reqTime) / (1000 * 60))); // Time difference in minutes
    return `${timeToGetRide-330} mins`;
  };

  if (loading) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>{error}</div>;
  }

  return (
    <>
    <NavBar/>
   

      <div className="h-screen flex flex-col justify-start py-12 px-12 sm:px-6 lg:px-8 pt-24">

        {/* Heading */}
        <h2 className="text-3xl font-bold text-white mb-6 text-center">Ride History</h2>

        {/* Render Ride History Cards in a 3-column grid */}
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6 w-full max-w-7xl px-4">
          {rideHistory.length > 0 ? (
          [...rideHistory].reverse().map((ride) => {
              const rideStart = new Date(ride.rideStartedAt);
              const reqTime = new Date(ride.reqTime);
              
              const formattedRideStart = formatDate(ride.rideStartedAt);
              const formattedReqTime = formatDate(ride.reqTime);

              const timeToGetRide = formatTimeDifference(rideStart, reqTime);
              const ttdr=timeToGetRide;
              // Determine who is the seeker and who is the helper
              const seekerName = isDriver ? ride.seekerUserName : ride.helperUserName;
              const helperName = isDriver ? ride.helperUserName : ride.seekerUserName;

              return (
                <div
                  key={ride.historyId}
                  className="p-6 bg-white border border-gray-300 rounded-lg shadow-lg"
                >
                  <div className="flex flex-col space-y-2">
                    <span className="text-gray-500 text-sm">🚗 Last {isDriver ? "Ride" : "Help"}</span>
                    <span className="font-semibold text-xl">
                      {ride.startPointName} → {ride.endPointName}
                    </span>
                    <div className="flex flex-col items-center justify-center space-y-2">
                      <span className="text-sm text-gray-600">
                        {ride.distance} km • {formattedRideStart} • with {seekerName}
                      </span>
                      <span className="text-xs text-gray-500">
                        📅 Requested {formattedReqTime} • Got ride in {ttdr}
                      </span>
                    </div>
                  </div>

                  {/* Action Button (Gift or Delete) */}
                  <div className="mt-3 flex justify-center">
                    {isDriver ? (
                      // If driver, display the received gift or show message accordingly
                      ride.rideRegardsFromSeeker ? (
                        <div className="bg-yellow-500 text-white px-5 py-2 rounded-full flex items-center space-x-2">
                          <Gift size={18} />
                          <span className="text-sm">{ride.rideRegardsFromSeeker} received as Gift : {ride.helpFromSeeker}</span>
                        </div>
                      ) : (
                        <span className="text-sm text-gray-600">No gift received yet</span>
                      )
                    ) : (
                      // If seeker, show Send Gift button
                      ride.helpFromSeeker === null && ride.rideRegardsFromSeeker === null ? (
                        <button
                          onClick={() => openModal(ride)} // Open modal when clicked
                          className="bg-green-500 hover:bg-green-600 text-white px-5 py-2 rounded-full flex items-center space-x-2"
                        >
                          <Gift size={18} />
                          <span className="text-sm">Send Gift</span>
                        </button>
                      ) :      <div className="bg-black text-white px-5 py-2 rounded-full flex items-center space-x-2">
                      <Gift size={18} />
                      <span className="text-sm">{ride.rideRegardsFromSeeker} Sended as Gift : {ride.helpFromSeeker}</span>
                    </div>
                    )}
                  </div>
                </div>
              );
            })
          ) : (
            <div>No ride history available.</div>
          )}
        </div>
      </div>

      {/* Modal Popup for Sending Gift */}
      {showModal && (
        <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50 z-50">
          <div className="bg-white p-6 rounded-lg shadow-lg max-w-sm w-full mt-10"> {/* Added mt-10 for margin-top */}
            <h3 className="text-xl font-semibold mb-4">Send Gift</h3>
            <div className="mb-4">
              <label className="block text-sm text-gray-600">Category</label>
              <input
                type="text"
                value={category}
                onChange={(e) => setCategory(e.target.value)}
                className="w-full p-2 border border-gray-300 rounded-md"
                placeholder="Enter category"
              />
            </div>
            <div className="mb-4">
              <label className="block text-sm text-gray-600">Help</label>
              <input
                type="text"
                value={help}
                onChange={(e) => setHelp(e.target.value)}
                className="w-full p-2 border border-gray-300 rounded-md"
                placeholder="Enter help details"
              />
            </div>
            <div className="flex justify-between">
              <button
                onClick={closeModal}
                className="bg-gray-400 hover:bg-gray-500 text-white px-5 py-2 rounded-full"
              >
                Cancel
              </button>
              <button
                onClick={handleGift}
                className="bg-green-500 hover:bg-green-600 text-white px-5 py-2 rounded-full"
              >
                Submit Gift
              </button>
            </div>
          </div>
        </div>
      )}
    </>
  );
};

export default History;
