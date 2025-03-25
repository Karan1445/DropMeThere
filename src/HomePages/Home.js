import { useEffect } from "react";
import CheckAcces from "../Auth-Jwt/CheckAcces";
import MapComponent from "../Maps/MapComponent";
import "./Home.css";
import NavBar from "../NavBar/NavBar";
import { Autocomplete } from "@react-google-maps/api";
function Home(){
    useEffect(()=>{
        CheckAcces();
    },[])
   
return(<>
<NavBar/>
<MapComponent/>

</>)
}
export default Home;