  import axios from "axios";
  import UserCard from "./UserCard";
  import { useEffect, useState } from "react";
  import DisplayErros from "../Toast-Erro-Display/DisplayErrors";
  import "../ViewSeekerRequest/Common.css"
  import NavBar from "../NavBar/NavBar";
  import { toast, ToastContainer } from "react-toastify"; 

  import CheckAcces from "../Auth-Jwt/CheckAcces";
  function ViewRequestList(){

      const [users, setUsers] = useState([]);
    
      useEffect(()=>{
        CheckAcces()
          fetchData();
      },[])
      
    function handleEdit(requestID){
          window.location.href="/RequestARide/"+requestID;
      
    }
    async function fetchData(){
      try {
        
          const token = localStorage.getItem('jwtToken'); // Retrieve the token from local storage
          const response = await axios.get('http://localhost:5036/api/SeekerRequestHandler/GetWithSpecificUser?UserID='+localStorage.getItem("UserID"), {
            headers: {
              Authorization: `Bearer ${token}`, // Include the Bearer token in the Authorization header
            },
          });
        
          const data = response.data;
          console.log(data)
          setUsers(data);
        } catch (error) {
          DisplayErros(error);
          console.error('Error fetching data:', error);
        }
      }
      async function handleDelete(RequestID) {
        if (window.confirm("Are you sure you want to delete this request?")) {
          try {
            const token = localStorage.getItem("jwtToken"); // Retrieve the token from local storage
            await axios
              .delete(`http://localhost:5036/api/SeekerRequestHandler?RequestID=${RequestID}`, {
                headers: {
                  Authorization: `Bearer ${token}`, // Include the Bearer token in the Authorization header
                },
              })
              .then(async (res) => {
                await toast.success("Deleted");
                fetchData();
              })
              .catch((error) => {
                DisplayErros(error);
                console.error("Error deleting data:", error);
              });
          } catch (error) {
            DisplayErros(error);
            console.error("Error deleting data:", error);
          }
        }
      }
      
  return (<>
  <NavBar/>
  <div className="h-screen flex flex-col justify-start text-white py-12 px-12 sm:px-6 lg:px-8">
    <div className="max-w-7xl mx-auto">
      <h1 className="text-4xl font-bold mb-8 mt-8 text-center">My Request List</h1>
      <div className="grid gap-5 grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4">
      
        {users.map((user) => (
          <UserCard key={user.requestID} user={user} onEdit={handleEdit} onDelete={handleDelete} />
        ))}
      </div>
    </div>
  </div>
  <ToastContainer/>
  </>)
  }
  export default ViewRequestList;