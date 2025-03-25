import { useNavigate } from 'react-router-dom';

function CheckVehicalRegistry(){
    const navigate=useNavigate();
    if(localStorage.getItem("IsDriver")=="Yes"){
        if(localStorage.getItem("IsVehicalRegis")=="No"){
            navigate("/dovehicalregistry");
        }
    }
}
export default CheckVehicalRegistry;