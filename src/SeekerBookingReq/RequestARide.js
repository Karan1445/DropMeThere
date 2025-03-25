import { useEffect, useState } from "react";
import CheckAcces from "../Auth-Jwt/CheckAcces";
import AutocompleteTextbox from "../SuggestionTextBoxs/AutocompleteTextbox";
import { toast, ToastContainer } from "react-toastify";
import axios from "axios";
import DisplayErros from "../Toast-Erro-Display/DisplayErrors";
import { useNavigate, useParams } from "react-router-dom";
import { Spline } from "lucide-react";

function RequestARide() {
  const navigate=useNavigate();
  if (localStorage.getItem("IsDriver") === "Yes") {
    navigate("/Home");
  }

  CheckAcces();

  const { RequestID } = useParams(); // Get RequestID from URL params
  const [startLocation, setStartLocation] = useState({});
  const [endLocation, setEndLocation] = useState({});
  const [formData, setFormData] = useState(null);
  const [startLocationName, setStartnameLocation] = useState(""); 
  const [endLocationName, setEndnameLocation] = useState(""); 
  useEffect(() => {
    console.log(RequestID)
    if (RequestID) {
      // Fetch existing data for editing
      axios
        .get(`http://localhost:5036/api/SeekerRequestHandler/GetWithSpecificRequest?RequestID=${RequestID}`, {
          headers: { Authorization: `Bearer ${localStorage.getItem("jwtToken")}` },
        })
        .then((response) => {
          const data = response.data;
          setStartnameLocation(data.startPointName)
          setEndnameLocation(data.endPointName)
          setStartLocation({
            name: data.startPointName,
            latitude: data.rideStartPointLatLong.split(",")[0],
            longitude: data.rideStartPointLatLong.split(",")[1],
          });
          setEndLocation({
            name: data.endPointName,
            latitude: data.rideEndPointLatLong.split(",")[0],
            longitude: data.rideEndPointLatLong.split(",")[1],
          });
          setFormData(data);
        })
        .catch((error) => {
          DisplayErros(error);
          console.error("Error fetching request data:", error);
        });
    }
  }, [RequestID]);

  const handleStartLocationChange = (input) => {
    setStartLocation(input);
  };

  const handleEndLocationChange = (input) => {
    setEndLocation(input);
  };

  async function doRequestSave() {
    const url = "http://localhost:5036/api/SeekerRequestHandler";
    const currentLocation = localStorage.getItem("CurrentLocationLatLong");
    const userName = localStorage.getItem("UserName");
    const phoneNumber = localStorage.getItem("PhoneNumber") || "0000000000";

    // Parse address components for seeker details
    const addressComponents = startLocation?.address_components || [];
    let seekerCity = "Unknown City";
    let seekerState = "Unknown State";
    let seekerArea = "Unknown Area";

    for (const component of addressComponents) {
        if (component.types.includes("locality")) {
            seekerCity = component.long_name;
        } else if (component.types.includes("administrative_area_level_1")) {
            seekerState = component.long_name;
        } else if (component.types.includes("administrative_area_level_3")) {
            seekerArea = component.long_name;
        }
    }

    const origin = `${startLocation.latitude},${startLocation.longitude}`;
    const destination = `${endLocation.latitude},${endLocation.longitude}`;

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

        // Prepare the data object
        const data = {
            requestID: RequestID,
            userID: parseInt(localStorage.getItem("UserID")),
            userName,
            phoneNumber,
            seekerCity,
            seekerState,
            seekerArea,
            currentLocationLatLong: currentLocation,
            rideStartPointLatLong: `${startLocation.latitude},${startLocation.longitude}`,
            rideEndPointLatLong: `${endLocation.latitude},${endLocation.longitude}`,
            reqTime: new Date().toISOString(),
            startPointName: startLocation.name,
            endPointName: endLocation.name,
            distance: RouteResponse.data.routes[0].legs[0].readable_distance,
            reachableTime: RouteResponse.data.routes[0].legs[0].readable_duration,
        };

        // Determine whether to POST (create) or PUT (edit)
        const axiosMethod = RequestID ? axios.put : axios.post;

        // Save the request
        await axiosMethod(url, data, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem("jwtToken")}`,
                "Content-Type": "application/json",
            },
        });

        // Wait for toast to display before navigating
        toast.success(RequestID ? "Request updated successfully!" : "Request created successfully!", {
            onClose: () => {
                navigate("/Home"); // navigate after the toast has finished displaying
            }
        });

    } catch (error) {
        DisplayErros(error);
        console.error("Error saving request data:", error);
    }
}

  return (
    <>
     <div className='spline-background'>
        <Spline scene="https://prod.spline.design/VQ9Wbl7u6fckcwGn/scene.splinecode" />
      </div>
      <form className="form" onSubmit={(e) => e.preventDefault()}>
        <p id="heading">
          <span>R</span>equest for ride!
        </p>
        <h6 style={{ color: "white" }}>Enter Start Location</h6>
    
        <AutocompleteTextbox onSelection={handleStartLocationChange} placeholder="Start Location" name="StartLocation" />

        <h6 style={{ color: "white" }}>Enter End Location</h6>
        <AutocompleteTextbox onSelection={handleEndLocationChange} placeholder="End Location" className="input-field"  />

        <div className="btn"></div>
        <button className="button3" onClick={doRequestSave}>
          {RequestID ? "Update Request" : "Request A Ride"}
        </button>
      </form>
      <ToastContainer />
    </>
  );
}

export default RequestARide;
