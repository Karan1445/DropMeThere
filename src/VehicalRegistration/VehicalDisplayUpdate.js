import React, { useEffect, useState } from "react";
import { ToastContainer, toast } from "react-toastify";
import axios from "axios";
import "react-toastify/dist/ReactToastify.css";
import CheckAcces from "../Auth-Jwt/CheckAcces";
import DisplayErros from "../Toast-Erro-Display/DisplayErrors";
import NavBar from "../NavBar/NavBar";
function VehicalDisplayUpdate() {
  CheckAcces();

  const [formData, setFormData] = useState({
    vehicalNumber: "",
    helperDL: "",
    helperLocality: "",
    vehicalName: "",
    vehicalType: "",
    vehicalColor: "",
    vehicalID:""
  });

  const [isEditable, setIsEditable] = useState(false);
  const userID = localStorage.getItem("UserID"); // Replace with a dynamic UserID if needed

  useEffect(() => {
    // Fetch data from the API with UserID
    axios
      .get(`http://localhost:5036/api/VehicalRegistration?UserID=${userID}`, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem("jwtToken")}`,
          "Content-Type": "application/json",
        },
      })
      .then((response) => {
        console.log(response);
        setFormData(response.data);
        toast.success("Data fetched successfully!");
      })
      .catch((error) => {
        console.error("Error fetching data:", error);
        toast.error("Failed to fetch data.");
      });
  }, [userID]);

  const handleInputChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const toggleEdit = () => {
    setIsEditable(!isEditable); // Toggle editing mode
  };

  const handleFormSubmit = (e) => {
    console.log(formData)
    e.preventDefault(); // Prevent the default form submission behavior
    // Ensure that this is only triggered on Save button click
    axios
      .put(`http://localhost:5036/api/VehicalRegistration`, formData, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem("jwtToken")}`,
          "Content-Type": "application/json",
        },
      })
      .then(() => {
        toast.success("Data updated successfully!");
        setIsEditable(false); // Disable inputs after saving
      })
      .catch((error) => {
        DisplayErros(error)
        console.error("Error updating data:", error);
        toast.error("Failed to update data.");
      });
  };

  return (
    <>
  <NavBar/>
      <form className="form" onSubmit={handleFormSubmit}>
        <p id="heading">
          {!isEditable?"View Your Vehicle":"Update Your Vehicle"}
        </p>

        {/* Vehicle Number */}
        <div className="field">
          <input
            autoComplete="on"
            placeholder="Vehicle Number"
            className="input-field"
            name="vehicalNumber"
            type="text"
            value={formData.vehicalNumber}
            onChange={handleInputChange}
            disabled={!isEditable}
          />
        </div>

        {/* Helper Driver License */}
        <div className="field">
          <input
            placeholder="Helper Driver License"
            className="input-field"
            type="text"
            name="helperDL"
            value={formData.helperDL}
            onChange={handleInputChange}
            disabled={!isEditable}
          />
        </div>

        {/* Helper Locality */}
        <div className="field">
          <input
            placeholder="Helper Locality"
            className="input-field"
            type="text"
            name="helperLocality"
            value={formData.helperLocality}
            onChange={handleInputChange}
            disabled={!isEditable}
          />
        </div>

        {/* Vehicle Name */}
        <div className="field">
          <input
            placeholder="Vehicle Name"
            className="input-field"
            type="text"
            name="vehicalName"
            value={formData.vehicalName}
            onChange={handleInputChange}
            disabled={!isEditable}
          />
        </div>

        {/* Vehicle Type */}
        <div className="field">
          <input
            placeholder="Vehicle Type"
            className="input-field"
            type="text"
            name="vehicalType"
            value={formData.vehicalType}
            onChange={handleInputChange}
            disabled={!isEditable}
          />
        </div>

        {/* Vehicle Color */}
        <div className="field">
          <input
            placeholder="Vehicle Color"
            className="input-field"
            type="text"
            name="vehicalColor"
            value={formData.vehicalColor}
            onChange={handleInputChange}
            disabled={!isEditable}
          />
        </div>

        {/* Buttons */}
        <div className="btn">
          {!isEditable ? (
            <button
              type="button"
              className="button1"
              onClick={toggleEdit} // Toggle edit mode on click
            >
              Enable Edit
            </button>
          ) : (
            <input className="button1" type="submit"/> 
            
            
          )}
        </div>
      </form>

      <ToastContainer />
    </>
  );
}

export default VehicalDisplayUpdate;
