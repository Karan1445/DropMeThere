import React, { useState, useEffect } from 'react';
import '../dist/style.css';  // Import SDK styles (adjust if necessary)
import { OlaMaps } from '../dist/olamaps-js-sdk.es.js';  // Import the OlaMaps SDK
import axios from 'axios';
import CheckAcces from '../Auth-Jwt/CheckAcces.js';
import { toast, ToastContainer } from 'react-toastify';
import { ParkingMeter } from 'lucide-react';
import DisplayErros from '../Toast-Erro-Display/DisplayErrors.js';
import { destination } from '@turf/turf';
import { data } from 'react-router-dom';
import RequestshowCard from '../HelperRequestList/RequestShowCard.js';
import { createRoot } from "react-dom/client";
const MapComponent = () => {
  const [currentLocation, setCurrentLocation] = useState(null);
  const extractedCoordinates = []
  const [startLocation,setstartLocation]=useState({});
  const [endLocation,setendLocation]=useState({});
  useEffect(() => {
   
    //Check User iD auth or not
    CheckAcces();
    //-------------------------------
    callTost();
    axios
    .post(
      'https://api.olamaps.io/routing/v1/directions?origin=22.3137564%2C70.8101449&destination=22.7803315%2C70.8516839&mode=driving&alternatives=false&steps=true&overview=full&language=en&traffic_metadata=false&api_key=zfREBrvVLEZgcmMDr5SXwI9qYyvTSrJymkqiZICy'
     ).then((response) => {
      console.log(response)
      response.data.routes[0].legs[0].steps.forEach(element => {
        extractedCoordinates.push([element.start_location.lng, element.start_location.lat]);
      });
      //console.log(extractedCoordinates)
      //console.log(extractedCoordinates)
    })
    //-------------------------------
 
   
  
          // Get the user's current location using Geolocation API
          if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(
              (position) => {
                const { latitude, longitude } = position.coords;
                setCurrentLocation({ lat: latitude, lng: longitude });
                localStorage.setItem("CurrentLocationLatLong",latitude+","+longitude);
                // Initialize Ola Map after getting the location
                initializeMap(latitude, longitude);
              },
              (error) => {
                console.error('Error getting location:', error);
                // Default location fallback if geolocation fails
                setCurrentLocation({ lat: 12.931423492103944, lng: 77.61648476788898 });
                initializeMap(12.931423492103944, 77.61648476788898);
              }
            );
          }
          const initializeMap = (latitude, longitude) => {
            const olaMaps = new OlaMaps({
              apiKey: 'zfREBrvVLEZgcmMDr5SXwI9qYyvTSrJymkqiZICy',
            });
        
            // Initialize the map with the user's location
            const myMap = olaMaps.init({
              style: 'https://api.olamaps.io/tiles/vector/v1/styles/default-dark-standard-hi/style.json',
              container: 'map', // The ID of the div where the map will be rendered
              center: [longitude, latitude], // User's current location [lng, lat]
              zoom: 15, // Initial zoom level
            });
        
            // Add a marker for the user's current location
            var olaIcon = document.createElement('div');
            olaIcon.classList.add('olalogo'); // Add a class for custom styling
        
            olaMaps
              .addMarker({ element: olaIcon, offset: [0, -10], anchor: 'bottom' })
              .setLngLat([longitude, latitude]) // Place the marker at the user's location
              .addTo(myMap);
        
            // Optional: Add a popup to the marker
            const popup = olaMaps
              .addPopup({ offset: [0, -30], anchor: 'bottom' })
              .setHTML('<div>This is your current location!</div>');
        
            olaMaps
              .addMarker({
                offset: [0, 6],
                anchor: 'bottom',
                color: 'red',
                draggable: true,
              })
              .setLngLat([longitude, latitude]) // Set popup at the marker location
              .setPopup(popup)
              .addTo(myMap);
          
        
              myMap.on('load', () => {
               
                myMap.addSource('route', {
                  type: 'geojson',
                  data: {
                    type: 'Feature',
                    properties: {},
                    geometry: {
                      type: 'LineString',
                      coordinates:[],
                    },
                  },
                })  
                myMap.addLayer({
                  id: 'route',
                  type: 'line',
                  source: 'route',
                  layout: { 'line-join': 'round', 'line-cap': 'round' },
                  paint: {
                    'line-color': '#f00',
                    'line-width': 4,
                  },
                })
              })
              var endlocation;
              var startlocation;
              
                myMap.on("click", (res) => {
                  if(localStorage.getItem("IsDriver")=="No"){
                  if(window.confirm("Are You Want To Raise A Request1")){
                    const params={
                      location :res.lngLat.lat+","+res.lngLat.lng,
                       api_key:"zfREBrvVLEZgcmMDr5SXwI9qYyvTSrJymkqiZICy",
                       radius:100
                    }
                    axios.get('https://api.olamaps.io/places/v1/nearbysearch',{params}).then(async(res1)=>{
                      const params={
                        place_id:res1.data.predictions[0].place_id,
                        api_key:"zfREBrvVLEZgcmMDr5SXwI9qYyvTSrJymkqiZICy"
                     }
                      await axios.get('https://api.olamaps.io/places/v1/details',{params}).then((res2)=>{
                        endlocation=res2.data;
                     })
                   }).then(()=>{
                    const params1={
                      location :localStorage.getItem("CurrentLocationLatLong").split(",")[0]+","+localStorage.getItem("CurrentLocationLatLong").split(",")[1],
                       api_key:"zfREBrvVLEZgcmMDr5SXwI9qYyvTSrJymkqiZICy",
                       radius:1000
                    }
                    axios.get('https://api.olamaps.io/places/v1/nearbysearch',{params:params1}).then(async(res1)=>{
                      const params={
                        place_id:res1.data.predictions[0].place_id,
                        api_key:"zfREBrvVLEZgcmMDr5SXwI9qYyvTSrJymkqiZICy"
                     }
                      await axios.get('https://api.olamaps.io/places/v1/details',{params}).then((res2)=>{
                        startlocation=res2.data;
                     })
                   }).then(async()=>{
                    try{
                    const origin = startlocation.result.geometry.location.lat+","+startlocation.result.geometry.location.lng;
                    const destination = endlocation.result.geometry.location.lat+","+endlocation.result.geometry.location.lng;
                
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
                    var seekerCity = "Unknown City";
                    var seekerState = "Unknown State";
                    var seekerArea = "Unknown Area";
               
                    for (const component of startlocation.result.address_components) {
                      if (component.types.includes("administrative_area_level_3")) {
                        seekerCity = component.long_name;
                      } else if (component.types.includes("administrative_area_level_1")) {
                        seekerState = component.long_name;
                      } else if (component.types.includes("administrative_area_level_2")) {
                        seekerArea = component.long_name;
                      }
                    }
                    const RouteResponse = await axios.post("https://api.olamaps.io/routing/v1/directions", null, { params });
                
                    // Prepare the data object
                    const data = {
                      userID: parseInt(localStorage.getItem("UserID")),
                      userName:localStorage.getItem("UserName"),
                      phoneNumber:localStorage.getItem("PhoneNumber"),
                      seekerCity:seekerCity,
                      seekerState:seekerState,
                      seekerArea:seekerArea,
                      currentLocationLatLong: localStorage.getItem("CurrentLocationLatLong"),
                      rideStartPointLatLong: origin,
                      rideEndPointLatLong: destination,
                      reqTime: new Date().toISOString(),
                      startPointName: startlocation.result.name,
                      endPointName: endlocation.result.name,
                      distance: RouteResponse.data.routes[0].legs[0].readable_distance,
                      reachableTime: RouteResponse.data.routes[0].legs[0].readable_duration,
                    };
                    
                    const url = "http://localhost:5036/api/SeekerRequestHandler";
                    axios.post(url, data, {
                      headers: {
                        Authorization: `Bearer ${localStorage.getItem("jwtToken")}`,
                        "Content-Type": "application/json",
                      },
                    })
                      .then(() => {
                        toast.success( "Request Raised successfully!" );
                      
                      })
                      .catch((error) => {
                        DisplayErros(error);
                        console.error("Error saving request data:", error);
                      });
                  } catch (error) {
                    DisplayErros(error);
                    console.error("Error calculating route:", error);
                  }
                   })
                   
                   })
                   //for destination--------------------------------------
                   
                  }
                }
                
                })
                if(localStorage.getItem("IsDriver")=="Yes"){
                   myMap.on('load',async()=>{
                    try {
                    
                      const token = localStorage.getItem('jwtToken'); // Retrieve the token from local storage
                      const response = await axios.get('http://localhost:5036/temporary', {
                        headers: {
                          Authorization: `Bearer ${token}`, // Include the Bearer token in the Authorization header
                        },
                      });
                   const data= response.data;
                 
                   //---adding marker with popup
                      if(data.length>0){
                        data.map(x=>{
                          var olaIcon = document.createElement('div');
                          olaIcon.classList.add('customMarkerClass');
                          
                          olaMaps
                            .addMarker({ element: olaIcon, offset: [0, -10], anchor: 'bottom' })
                            .setLngLat([x.rideStartPointLatLong.split(",")[1],x.rideStartPointLatLong.split(",")[0]]) // Place the marker at the user's location
                            .addTo(myMap);
                            //------------gpt---ans
                            const popupContent = document.createElement("div"); // Create a new div element
                              document.body.appendChild(popupContent); // Append it to the body or another container
        
                              const root = createRoot(popupContent);
                              root.render(<RequestshowCard user={x} handleOnClick={(res) => {if(window.confirm("Ara you Sure!")){
                                doRequestConfirmation(res);
                              }}} />);
                          // Optional: Add a popup to the marker
                          const popup = olaMaps
                            .addPopup({ offset: [0, -30], anchor: 'bottom' })
                            .setDOMContent(popupContent);
                        
                          olaMaps
                            .addMarker({
                              offset: [0, 6],
                              anchor: 'bottom',
                              color: 'cyan',
                              draggable: false,
                            })
                            .setLngLat([x.rideStartPointLatLong.split(",")[1],x.rideStartPointLatLong.split(",")[0]]) // Set popup at the marker location
                            .setPopup(popup)
                            .addTo(myMap);
                          
                        })
                      }
                      //--------------------
                    } catch (error) {
                      DisplayErros(error);
                      console.error('Error Requests data:', error);
                    }
                 
                   })
                }
               
                async  function doRequestConfirmation(data){
            
                  var origin=localStorage.getItem("CurrentLocationLatLong");
                  var destination=data.rideStartPointLatLong
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
                  try{
                  const RouteResponse = await axios.post("https://api.olamaps.io/routing/v1/directions", null, { params });
                  const objectForPost={
                    seekerUserID: data.userID,
                    helperUserID: localStorage.getItem("UserID"),
                    requestID: data.requestID,
                    helpersCurrentLocationLatLong: origin, // Example coordinates
                    confirmationTime: new Date().toISOString(),
                    helperRechabletimetoStartPoint: RouteResponse.data.routes[0].legs[0].readable_duration, // Example time
                    helperDistanceFromStartPoint:  RouteResponse.data.routes[0].legs[0].readable_distance, // Example distance
                  }
                 const reply= await axios.post('http://localhost:5036/api/HelperSideViewRides',objectForPost, {
                    headers: {
                      Authorization: `Bearer ${ localStorage.getItem('jwtToken')}`, 
                    },
                  }).then(()=>{
                    toast.success("Help Assigned to you!")
                  })
                  console.log(reply)
                  }catch(error){
                    DisplayErros(error)
                    console.log(error)
                  }
                  }
          };
  }, []);
async function callTost(){
  if(localStorage.getItem("isLoggedIn")==="999"){
    await toast.success("Hello User! " +localStorage.getItem("UserName"),{autoClose:2000});
   localStorage.setItem("isLoggedIn","-1")
 }
 if(localStorage.getItem("isLoggedIn")==="9991"){
  await toast.success("Welcome! " +localStorage.getItem("UserName"),{autoClose:2000});
 localStorage.setItem("isLoggedIn","-1")
}
}
 

  return (
    <div>
      <div
        id="map"
        style={{
          zIndex:1,
          width: '100vw',
          height: '100vh',
          position: 'absolute', // Ensures it spans the full viewport
          top: 0,
          left: 0,
        }}
      ></div>
      {!currentLocation && <div>Loading your location...</div>}
      <ToastContainer/>
      
    </div>
  
  );
};

export default MapComponent;
