import axios from "axios";
import DisplayErrors from "../Toast-Erro-Display/DisplayErrors";
import { useEffect } from "react";

import useCheckVehicalRegistry from "../VehicalRegistration/CheckVehicalRegistry";
import { useNavigate } from "react-router-dom";

function CheckAcces() {
 
  try { 
    
    // Retrieve the JWT token from localStorage
    const token = localStorage.getItem("jwtToken");

    if (token) {
      // Send a request to the API to validate the JWT token
      axios
        .get("http://localhost:5036/api/LoginRegisters", {
          headers: {
            Authorization: `Bearer ${token}`, // Sending JWT in Authorization header
          },
        })
        .then((response) => {
          console.log("Token is valid:", response.data);
          
        if(localStorage.getItem("IsDriver")=="Yes"){
            if(localStorage.getItem("IsVehicalRegis")=="No"){
              window.location.href="/dovehicalregistry";
            }
    }
          // You can add additional logic here if needed
        })
        .catch((error) => {
          if (error.response) {
            console.error("Token validation failed:", error.response);

            // Handle invalid token (e.g., redirect to login page if token is invalid or expired)
            if (error.response.status === 401) {
              localStorage.removeItem("jwtToken"); // Remove invalid token
              window.location.href = "/login"; // Redirect to login page
            }
          } else if (error.request) {
            // Server did not respond (e.g., server is down)
            console.error("No response received from server:", error.request);
            DisplayErrors({ data: "Server is down! Please try again later." });
          } else {
            // Other errors
            console.error("Error during token validation:", error.message);
            DisplayErrors({ data: "Serevr is busy!!!" });
          }
        });
    } else {
      console.log("No token found");

      // Handle the case where no token exists (Redirect to login)
      window.location.href = "/login";
    }
  } catch (err) {
    console.error("Unexpected error:", err);
    DisplayErrors({ data: "An unexpected error occurred while checking access." });
  }

}

export default CheckAcces;
