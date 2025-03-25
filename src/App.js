
import './App.css';

import { BrowserRouter, Routes, Route } from "react-router-dom";
import Entrypage  from './Pages/Entrypage';
import AutocompleteTextbox from './SuggestionTextBoxs/AutocompleteTextbox';

import MapComponent from './Maps/MapComponent';
import Checkvalidroute from './Tetsing Pages/Checkvalidroute';
import LoginForm from './LoginRegisteration/LoginForm';
import Registration from './LoginRegisteration/Registration';
import Home from './HomePages/Home';
import DoVehicalRegister from './VehicalRegistration/RegistryPage/DoVehicalRegister';
import NavBar from './NavBar/NavBar';
import RequestARide from './SeekerBookingReq/RequestARide';
import ViewRequestList from './ViewSeekerRequest/ViewRequestList';
import VehicalDisplayUpdate from './VehicalRegistration/VehicalDisplayUpdate';
import ViewSeekerRequest from './HelperRequestList/ViewSeekersRequests';
import MyActiveRequest from './ViewActiveRequests/MyActiveRequest';
import History from './History/History';
import UserProfile from './UserProfile/UserProfile';
import ForgetPass from './LoginRegisteration/ForgetPass';
import HomePage from './admin/HomePage';
import AdminDashboard from './admin/AdminDashboard';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Entrypage/>}/>
        <Route path="/Home" element={<Home/>} />
        <Route path="/testsite" element={<Checkvalidroute/>}/>
        <Route path="/Login" element={<LoginForm/>}/>
        <Route path="/Registration" element={<Registration/>}/>
        <Route path="/forgetpassword" element={<ForgetPass/>}/>
        <Route path="/dovehicalregistry" element={<DoVehicalRegister/>}/>
        <Route path="/Requestaride/:RequestID?" element={<RequestARide/>}/>
        <Route path="/ViewSeekersRequest" element={<ViewSeekerRequest/>}/>
        <Route path="/ViewRequest" element={<ViewRequestList/>}/>
        <Route path="/MyActiveRequest" element={<MyActiveRequest/>}/>
        <Route path="/ViewVehical" element={<VehicalDisplayUpdate/>}/>
        <Route path='/History' element={<History/>}/>
        <Route path='/User' element={<UserProfile/>}/>
        <Route path="/Admin" element={<AdminDashboard/>}/>
        <Route path="/Admin/Home" element={<AdminDashboard/>}/>
        <Route path="/Admin/Dashboard" element={<AdminDashboard/>}/>
      </Routes>
    </BrowserRouter>
  );
}

export default App;