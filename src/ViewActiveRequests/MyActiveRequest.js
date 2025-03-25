import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { User, MapPin, Navigation, Clock, CheckCircle, NavigationOff ,Phone} from 'lucide-react'; // Assuming these are imported from a library
import NavBar from '../NavBar/NavBar';
import { toast, ToastContainer } from 'react-toastify';
import DisplayErros from '../Toast-Erro-Display/DisplayErrors';
import {useNavigate } from "react-router-dom";
function MyActiveRequest(){
    const [rideData, setRideData] = useState(null);
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate()
  useEffect(() => {
   

    const fetchRideData = async () => {
      try {
        const response = await axios.get(
          'http://localhost:5036/api/HelperSideViewRides?UserID='+localStorage.getItem("UserID"),
          {
            headers: {
              Authorization: `Bearer ${localStorage.getItem("jwtToken")}`, // Add your actual token here
            },
          }
        );
      console.log(response)
        setRideData(response.data); // Set the fetched data to state
        setLoading(false); // Stop loading
      } catch (error) {
        console.error('Error fetching ride data:', error);
        setLoading(false);
      }
    };

    fetchRideData();
  }, []);
async function FinishRide(){
  if(window.confirm("Your Ride iS overd?")){
  try{
    const response= await axios.delete('http://localhost:5036/api/History?id='+rideData.confrimationID,
        {
          headers: {
            Authorization: `Bearer ${localStorage.getItem("jwtToken")}`, // Add your actual token here
          },
        }
      )
      console.log(rideData.con)
  await toast.warn("Ride is Over!")
  navigate("/Home");
 
}catch(e){
  console.log("hii")
  DisplayErros(e);
}
  }
}


  const onRideFinish = () => {
    
    console.log('Ride Finished');
  };

  if (loading) {
    return <div className="text-center text-white">Loading data & details...</div>;
  }

  if (!rideData) {
    return <div className="text-center text-white"><NavBar/>No data available</div>;
  }

  const {
    seekerUserName,
    seekerPhoneNumber,
    helperUserName,
    helperPhoneNumber,
    startPointName,
    endPointName,
    helperDistanceFromStartPoint,
    helperRechabletimetoStartPoint,
    ridedataDistance,
    ridedataReachableTime,
  } = rideData;
  
    return (
        <>
        <NavBar/>
        <div className="min-h-screen p-6 mt-10 flex justify-center items-center overflow-auto scrollbar-hide">
          <div className="w-full max-w-lg bg-gray-800 rounded-lg shadow-xl overflow-hidden">
            <div className="p-4 sm:p-6 space-y-6">
              <h2 className="text-2xl sm:text-3xl font-semibold text-center text-purple-400">Ride Details</h2>
              <div className="space-y-4">
                {localStorage.getItem("IsDriver")!="No"?<div className="flex items-center space-x-3">
                  <User className="text-green-400" />
                  <div>
                    <p className="font-semibold text-sm sm:text-base text-white">Seeker: {seekerUserName}</p>
                    <p className="text-xs sm:text-sm text-gray-400">{seekerPhoneNumber}</p>
                  </div>
                </div>
                :""}
                {localStorage.getItem("IsDriver")=="No"? <div className="flex items-center space-x-3">
                  <User className="text-blue-400" />
                  <div>
                    <p className="font-semibold text-sm sm:text-base text-white">Helper: {helperUserName}</p>
                    <p className="text-xs sm:text-sm text-gray-400">{helperPhoneNumber}</p>
                  </div>
                </div>:""}
               
                <div className="flex items-center space-x-3">
                  <MapPin className="text-red-400" />
                  <div>
                    <p className="font-semibold text-sm sm:text-base text-white">Start: {startPointName}</p>
                  </div>
                </div>
                <div className="flex items-center space-x-3">
                  <MapPin className="text-yellow-400" />
                  <div>
                    <p className="font-semibold text-sm sm:text-base text-white">End: {endPointName}</p>
                  </div>
                </div>
                <div className="flex items-center space-x-3">
                  <Navigation className="text-indigo-400" />
                  <div>
                    <p className="font-semibold text-sm sm:text-base text-white">Helper's Distance: {helperDistanceFromStartPoint} km</p>
                    <p className="text-xs sm:text-sm text-gray-400">ETA: {helperRechabletimetoStartPoint}</p>
                  </div>
                </div>
                <div className="flex items-center space-x-3">
                  <Clock className="text-pink-400" />
                  <div>
                    <p className="font-semibold text-sm sm:text-base text-white">Ride Distance: {ridedataDistance} km</p>
                    <p className="text-xs sm:text-sm text-gray-400">ETA: {ridedataReachableTime}</p>
                  </div>
                </div>
              </div>
            </div>
            {localStorage.getItem("IsDriver")=="Yes"?
            <div className="px-6 py-4 bg-gray-900">
              <button
               onClick={FinishRide}
                className="w-full py-3 px-4 bg-gradient-to-r from-purple-500 to-pink-500 text-white font-bold rounded-lg shadow-lg hover:from-purple-600 hover:to-pink-600 transition duration-300 ease-in-out transform hover:scale-105 focus:outline-none focus:ring-2 focus:ring-purple-500 focus:ring-opacity-50"
              >
                <div className="flex items-center justify-center space-x-2">
                  <CheckCircle className="w-5 h-5" />
                  <span className="text-sm sm:text-base" >Ride is Finished?</span>
                </div>
              </button>
              <div className="mt-4">
              <button
                onClick={() => window.location.href = `tel:${localStorage.getItem("PhoneNumber") || '0000000000'}`}
                className="w-full py-3 px-4 bg-gradient-to-r from-green-500 to-blue-500 text-white font-bold rounded-lg shadow-lg hover:from-green-600 hover:to-blue-600 transition duration-300 ease-in-out transform hover:scale-105 focus:outline-none focus:ring-2 focus:ring-green-500 focus:ring-opacity-50"
              >
                <div className="flex items-center justify-center space-x-2">
                  <Phone className="w-5 h-5" />
                  <span className="text-sm sm:text-base">Contact Helper</span>
                </div>
              </button>
            </div>
            </div>
            
            :<div className="mt-4">
              <button
                onClick={() => window.location.href = `tel:${localStorage.getItem("PhoneNumber") || '0000000000'}`}
                className="w-full py-3 px-4 bg-gradient-to-r from-green-500 to-blue-500 text-white font-bold rounded-lg shadow-lg hover:from-green-600 hover:to-blue-600 transition duration-300 ease-in-out transform hover:scale-105 focus:outline-none focus:ring-2 focus:ring-green-500 focus:ring-opacity-50"
              >
                <div className="flex items-center justify-center space-x-2">
                  <Phone className="w-5 h-5" />
                  <span className="text-sm sm:text-base">Contact Helper</span>
                </div>
              </button>
            </div>}
               
          </div>
        </div><ToastContainer/></>
      );
    
}
export default MyActiveRequest;