import React from 'react';
import './Page-css/Entrypage.css'; // Import your CSS file
import { useNavigate } from 'react-router-dom'; 
import Spline from '@splinetool/react-spline';
function Entrypage() {
    const navigate = useNavigate();
    
    function redirecttohome(){
         navigate("/Home");
    }
  return (
    <div className="entry-page">
    <div className='spline-background'>
      <Spline scene="https://prod.spline.design/sZnYQBPdfEzF0BSk/scene.splinecode" />
    </div>
      <h1> <span class="rainbow-text">W</span>elcome to Our Service!</h1>
      <p>Enjoy a free ride anytime</p>
      <button class="start-button" onClick={redirecttohome}>Start Here</button>
    </div>
  );
}

export default Entrypage;
// import Spline from '@splinetool/react-spline/next';

// export default function Home() {
//   return (
//     <main>
//       <Spline
//         scene="https://prod.spline.design/y8Cp2TZHFqBas6Ls/scene.splinecode" 
//       />
//     </main>
//   );
// }
