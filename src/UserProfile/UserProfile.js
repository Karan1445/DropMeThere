import React, { useState, useEffect } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import NavBar from "../NavBar/NavBar";

const UserProfile = () => {
  const [user, setUser] = useState(null);
  const [isEditing, setIsEditing] = useState(false);
  const navigate = useNavigate();

  // Fetch user data directly using localStorage
  useEffect(() => {
    const fetchUser = async () => {
      try {
        const userID = localStorage.getItem("UserID");
        if (!userID) {
          console.warn("UserID not found in localStorage.");
          return;
        }
  
        const response = await axios.get(`http://localhost:5036/GetAUser?UserID=${userID}`);
        setUser(response.data);
      } catch (error) {
        console.error("Error fetching user data:", error);
      }
    };
  
    fetchUser();
  }, []);
  

  // Handle update
  const handleUpdate = () => {
    axios
      .put(`http://localhost:5036/api/Users`, user)
      .then((response) => {
        setUser(response.data);
        setIsEditing(false);
      })
      .catch((error) => console.error("Error updating user:", error));
  };

  // Handle delete
  const handleDelete = () => {
    axios
      .delete(`http://localhost:5036/api/Users?UserID=${localStorage.getItem("UserID")}`)
      .then(() => {
        localStorage.removeItem("UserID");
        navigate("/");
      })
      .catch((error) => console.error("Error deleting account:", error));
  };

  if (!user) return <p className="text-white text-center">Loading...</p>;

  return (
    <>
    <NavBar/>
    <div className="flex justify-center items-center h-screen">
      {/* User Info Card */}
      <div className="p-6 rounded-lg shadow-md w-full max-w-md border border-gray-700 text-white">
        <h2 className="text-2xl font-semibold mb-4">Profile</h2>
        <div className="space-y-2">
          <p><strong>Username:</strong> {user.UserName}</p>
          <p><strong>Email:</strong> {user.Email}</p>
          <p><strong>Phone:</strong> {user.PhoneNumber}</p>
          <p><strong>Password:</strong> {user.PassWord}</p>
          <p><strong>Driver:</strong> {user.IsDriver}</p>
          <p><strong>Vehicle Registered:</strong> {user.IsVehicalRegistered}</p>

        </div>

        {/* Action Buttons */}
        <div className="mt-4 flex justify-between">
          <button
            onClick={() => setIsEditing(true)}
            className="px-4 py-2 rounded border border-blue-400 hover:bg-blue-500 hover:text-black transition"
          >
            Update
          </button>
          <button
            onClick={handleDelete}
            className="px-4 py-2 rounded border border-red-400 hover:bg-red-500 hover:text-black transition"
          >
            Remove Account
          </button>
          <button
            onClick={() => navigate("/history")}
            className="px-4 py-2 rounded border border-gray-400 hover:bg-gray-500 hover:text-black transition"
          >
            View History
          </button>
        </div>
      </div>

      {/* Edit Popup */}
      {isEditing && (
        <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-60">
          <div className="bg-black text-white p-6 rounded-lg shadow-md w-full max-w-md border border-gray-700">
            <h2 className="text-2xl font-semibold mb-4">Edit Profile</h2>
            <div className="space-y-2">
              <input
                type="text"
                value={user.UserName}
                onChange={(e) => setUser({ ...user, UserName: e.target.value })}
                className="w-full p-2 border rounded bg-gray-800"
                placeholder="Username"
              />
              <input
                type="email"
                value={user.Email}
                onChange={(e) => setUser({ ...user, Email: e.target.value })}
                className="w-full p-2 border rounded bg-gray-800"
                placeholder="Email"
              />
              <input
                type="text"
                value={user.PhoneNumber}
                onChange={(e) => setUser({ ...user, PhoneNumber: e.target.value })}
                className="w-full p-2 border rounded bg-gray-800"
                placeholder="Phone Number"
              />
              <input
                type="password"
                value={user.PassWord}
                onChange={(e) => setUser({ ...user, PassWord: e.target.value })}
                className="w-full p-2 border rounded bg-gray-800"
                placeholder="Password"
              />
              <select
                value={user.IsDriver}
                onChange={(e) => setUser({ ...user, IsDriver: e.target.value })}
                className="w-full p-2 border rounded bg-gray-800"
              >
                <option value="Yes">Yes</option>
                <option value="No">No</option>
              </select>
              <select
                value={user.IsVehicalRegistered}
                onChange={(e) => setUser({ ...user, IsVehicalRegistered: e.target.value })}
                className="w-full p-2 border rounded bg-gray-800"
              >
                <option value="Yes">Yes</option>
                <option value="No">No</option>
              </select>
            </div>

            {/* Popup Action Buttons */}
            <div className="mt-4 flex justify-between">
              <button
                onClick={handleUpdate}
                className="px-4 py-2 rounded border border-green-400 hover:bg-green-500 hover:text-black transition"
              >
                Save
              </button>
              <button
                onClick={() => setIsEditing(false)}
                className="px-4 py-2 rounded border border-gray-400 hover:bg-gray-500 hover:text-black transition"
              >
                Cancel
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
    </>
  );
};

export default UserProfile;
