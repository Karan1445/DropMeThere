import { toast, ToastContainer } from 'react-toastify'; // Ensure correct import
import 'react-toastify/dist/ReactToastify.css'; // Toastify CSS
import  "./ToastCustomcss.css"
function DisplayErros(err){
    
   

    if (err.response) {
      const { data, status, statusText } = err.response;
  
      // Debugging: Log the response data
  
      // Handle validation errors (if `errors` field exists)
      if (data.errors) {
       
        const { errors } = data;
        Object.values(errors).forEach((messageArray) => {
        
          if (messageArray.length > 0) {
            
            toast.error(messageArray[0]); // Show the first error message
        }
        });
      }
  
      // Handle general `data` error messages
      if (data && typeof data === 'string') {
        toast.error(data); // Display error in the `data` field
      }
  
      // Handle HTTP status and status text
    //   if (status && statusText) {
    //     toast.error(`Error ${status}: ${statusText}`); // Show status and statusText
    //   }
    } else {
      // Fallback: Handle errors without `response`
      console.error("Error:", err); // Debugging: Log the raw error
      toast.error("An unknown error occurred."); // Display generic error message
    }
      };
export default DisplayErros;