import AutocompleteTextbox from "../SuggestionTextBoxs/AutocompleteTextbox";
import { useState,useEffect } from "react";
import axios from 'axios';
import { Route } from "react-router-dom";
import RouteComparison from "./RouteComparison";
import   CheckAcces from "../Auth-Jwt/CheckAcces"
function Checkvalidroute() {
 
  // State variables for each location
  const [helperStartLocation, setHelperStartLocation] = useState({});
  const [helperEndLocation, setHelperEndLocation] = useState({});
  
    const [clientStartLocation, setClientStartLocation] = useState({});
    const [clientEndLocation, setClientEndLocation] = useState({});
    
    const [result, setResult] = useState('');
  function handleSelectionHelperStartLocation(location) {
    setHelperStartLocation(location.geometry.location);
  }

  function handleSelectionHelperEndLocation(location) {
    setHelperEndLocation(location.geometry.location);
  }

  function handleSelectionClientStartLocation(location) {
    setClientStartLocation(location.geometry.location);
    }

    function handleSelectionClientEndLocation(location) {
      setClientEndLocation(location.geometry.location);
    }
  async function getRoutes(getStartlocation,getEndlocation){
    try{
        const apiKey = 'zfREBrvVLEZgcmMDr5SXwI9qYyvTSrJymkqiZICy';
       
        const origin = 24.814657+','+74.043724; // Replace with dynamic values if needed
        const destination = 20.599877+','+78.968885; // Replace with dynamic values if needed
        
          var params = {
            origin,
            destination,
            mode: 'driving',
            alternatives: false,
            steps: true,
            overview: 'full',
            language: 'en',
            traffic_metadata: false,
            api_key: 'zfREBrvVLEZgcmMDr5SXwI9qYyvTSrJymkqiZICy',
          };
      
        
        const RouteResponse=await axios.post('https://api.olamaps.io/routing/v1/directions', null, {params}); 
        console.log(RouteResponse.data.routes[0].legs[0]);
          return RouteResponse;
    }
    catch(err){
        console.log(err);
    }
}
  //https://api.olamaps.io/routing/v1/directions?origin=12.993103152916301%2C77.54332622119354&destination=12.993103152916301%2C77.54332622119354&mode=driving&alternatives=false&steps=true&overview=full&language=en&traffic_metadata=false&api_key=zfREBrvVLEZgcmMDr5SXwI9qYyvTSrJymkqiZICy
async function getbothrouteresponse(){
    const helperRoute = await getRoutes(helperStartLocation, helperEndLocation);
    const withClientRoute = await getRoutes(helperStartLocation, helperEndLocation);
    const polyline1=helperRoute.data.routes[0].overview_polyline;
    const polyline2=withClientRoute.data.routes[0].overview_polyline;

    const result = RouteComparison(polyline1, polyline2);
    setResult(result);
 }

  async function  handleSubmit(){
  await getbothrouteresponse()
}
 
  return (
    <>
      <h1>Helper Locations</h1>
      <h6>Start Point:</h6>
      <AutocompleteTextbox onSelection={getRoutes} />
      <h6>End Point:</h6>
      <AutocompleteTextbox onSelection={handleSelectionHelperEndLocation} />
      <h1>Seeker Locations</h1>
      <h6>Start Point:</h6>
      <AutocompleteTextbox onSelection={handleSelectionClientStartLocation} />
      <h6>End Point:</h6>
      <AutocompleteTextbox onSelection={handleSelectionClientEndLocation} />
      <button onClick={handleSubmit}>Log All Locations</button>
      <p>Result: {result}</p>
    </>
  );
}

export default Checkvalidroute;
