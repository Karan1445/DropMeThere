import { Link } from "react-router-dom";
import MapComponent from "../Maps/MapComponent"
import "../NavBar/NavBar.css"
import React, { useState } from "react";
import { toast, ToastContainer } from "react-toastify";
import { Check } from "lucide-react";
import CheckAcces from "../Auth-Jwt/CheckAcces";
import CheckVehicalRegistry from "../VehicalRegistration/CheckVehicalRegistry";
export default function NavBar(){
CheckVehicalRegistry();
  const [isMenuOpen, setIsMenuOpen] = useState(false);

  const toggleMenu = () => {
    setIsMenuOpen(!isMenuOpen);
  };
    return(<>
   
    <nav className={`mask ${isMenuOpen ? "menu-open" : ""}`}>
        <Link to={"/Home"}>dropmethere</Link>
        <button
          className="menu"
          aria-label="Toggle navigation menu"
          onClick={toggleMenu}
        >
          <div className="hamburger-icon">
            <span></span>
            <span></span>
            <span></span>
          </div>
        </button>
        <ul className={`list ${isMenuOpen ? "show" : ""}`}>
      
          {localStorage.getItem("IsDriver")=='Yes'?<li><Link to={"/ViewVehical"}>MyVehical</Link></li>:<></>}
          {localStorage.getItem("IsDriver")=='Yes'?<li><Link to={"/ViewSeekersRequest"} >View Requests</Link></li>:<li><Link to={"/Requestaride"}>Raise a Request</Link></li>}
          {localStorage.getItem("IsDriver")=='Yes'?<li><Link to={"/MyActiveRequest"}>Active Helps</Link></li>:<li><Link to={"/ViewRequest"} >My Requests</Link></li>}
          {localStorage.getItem("IsDriver")=='Yes'?"":<li><Link to={"/MyActiveRequest"}>Active Helps</Link></li>}
          <li><Link to={"/History"}>History</Link></li>
          <li><button onClick={async()=>{
            if(window.confirm("Your Seesion is been Loggedout!")){
              await toast.warn("Your Session is Over!");
              localStorage.clear();
              CheckAcces();
            }
          }}>Logout</button></li>
         
        </ul>
        
      </nav>

 
</>
      )
}