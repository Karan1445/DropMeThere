import '../LoginRegisteration/Login.css';
import Spline from '@splinetool/react-spline';
import axios from 'axios';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { toast, ToastContainer } from 'react-toastify'; // Ensure correct import
import 'react-toastify/dist/ReactToastify.css'; // Toastify CSS
import DisplayErros from '../Toast-Erro-Display/DisplayErrors';

function LoginForm() {
 
  const [LoginCredentials, setLCR] = useState({});
  const navigate = useNavigate();

  // Handle form data input change
  function handleInputData(e) {
    setLCR((prev) => ({
      ...prev,
      [e.target.name]: e.target.value,
    }));
  }

  // Function to make API request
  async function LoginPostData() {
      if(LoginCredentials.email=="admin@admin.com" && LoginCredentials.PassWord=="adminadmin"){
        localStorage.setItem("adminID",1)
        navigate("/Admin");
      }
    await axios
      .post(
        "http://localhost:5036/api/LoginRegisters", // API endpoint
        LoginCredentials,
        {
          headers: {
            "Content-Type": "application/json", // JSON content type
          },
        }
      )
      .then((res) => {
        localStorage.clear();
        localStorage.setItem("jwtToken", res.data.token);
        localStorage.setItem("UserID", res.data.user.userID);
        localStorage.setItem("Email", res.data.user.email);
        localStorage.setItem("UserName", res.data.user.userName);
        localStorage.setItem("PhoneNumber", res.data.user.phoneNumber);
        localStorage.setItem("IsDriver", res.data.user.isDriver);
        localStorage.setItem("IsVehicalRegis", res.data.user.isVehicalRegistered);
      })
      .then(() => {
        localStorage.setItem("isLoggedIn","999");
        navigate('/Home');
      })
      .catch((err) => {
        console.log("Error Response:", err.response); // Log the error response for debugging

       DisplayErros(err);
      });
  }

  return (
    <>
      {/* <div className='spline-background'>
        <Spline scene="https://prod.spline.design/VQ9Wbl7u6fckcwGn/scene.splinecode" />
      </div> */}

      <form className="form" onSubmit={(e) => e.preventDefault()}>
        <p id="heading"><span>L</span>ogin</p>
        <div className="field">
          <svg
            className="input-icon"
            xmlns="http://www.w3.org/2000/svg"
            width="16"
            height="16"
            fill="currentColor"
            viewBox="0 0 16 16"
          >
            <path d="M13.106 7.222c0-2.967-2.249-5.032-5.482-5.032-3.35 0-5.646 2.318-5.646 5.702 0 3.493 2.235 5.708 5.762 5.708.862 0 1.689-.123 2.304-.335v-.862c-.43.199-1.354.328-2.29.328-2.926 0-4.813-1.88-4.813-4.798 0-2.844 1.921-4.881 4.594-4.881 2.735 0 4.608 1.688 4.608 4.156 0 1.682-.554 2.769-1.416 2.769-.492 0-.772-.28-.772-.76V5.206H8.923v.834h-.11c-.266-.595-.881-.964-1.6-.964-1.4 0-2.378 1.162-2.378 2.823 0 1.737.957 2.906 2.379 2.906.8 0 1.415-.39 1.709-1.087h.11c.081.67.703 1.148 1.503 1.148 1.572 0 2.57-1.415 2.57-3.643zm-7.177.704c0-1.197.54-1.907 1.456-1.907.93 0 1.524.738 1.524 1.907S8.308 9.84 7.371 9.84c-.895 0-1.442-.725-1.442-1.914z"></path>
          </svg>
          <input
            onChange={(e) => handleInputData(e)}
            autoComplete="off"
            placeholder="Email"
            className="input-field"
            name="email"
            type="email"
          />
        </div>

        <div className="field">
          <svg
            className="input-icon"
            xmlns="http://www.w3.org/2000/svg"
            width="16"
            height="16"
            fill="currentColor"
            viewBox="0 0 16 16"
          >
            <path d="M8 1a2 2 0 0 1 2 2v4H6V3a2 2 0 0 1 2-2zm3 6V3a3 3 0 0 0-6 0v4a2 2 0 0 0-2 2v5a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V9a2 2 0 0 0-2-2z"></path>
          </svg>
          <input
            onChange={(e) => handleInputData(e)}
            placeholder="Password"
            className="input-field"
            type="password"
            name="PassWord"
          />
        </div>

        <div className="btn">
          <button className="button1" onClick={LoginPostData}>Login</button>
          <button className="button2" onClick={() => { navigate('/registration'); }}>Sign Up</button>
        </div>
        <button className="button3" onClick={() => { navigate('/forgetpassword'); }}>Forgot Password</button>
      </form>

      {/* ToastContainer for displaying toasts */}
      <ToastContainer />
    </>
  );
}

export default LoginForm;
