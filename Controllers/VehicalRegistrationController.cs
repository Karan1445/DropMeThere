using dropmethereapi.Models;
using dropmethereapi.Repos.VehicalRegistration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace dropmethereapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VehicalRegistrationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly VehicalRegistration _vehicalreg;

        public VehicalRegistrationController(IConfiguration icn, VehicalRegistration vcn) {
            _configuration = icn;
            _vehicalreg = vcn;
        }
        [HttpPost("RegisterVehical")]
       
        public IActionResult RegisterNewVehical(VehicalRegistrationModel data) {
            var response = _vehicalreg.RegisterVehical(data);
            Console.Write(response.UserName);
            if (response.UserName == _configuration["VehicalRegisrationErrordu:plicateNumberPlate"]) {
                return NotFound("Duplicate User Found Or User Is Already Registred Kindly Update the User!");
            }
            if (response.UserName == _configuration["VehicalRegisrationErrordu:duplicateEntry"])
            {
                return NotFound("User already Verified Found!");
            }
            if (response.UserName == _configuration["VehicalRegisrationErrordu:userNotFound"])
            {
                return NotFound("No User Found");
            }
            
             if (response.UserName == "Some---Error--occured!")
            {
                return NotFound("Inrernal Server Error");
            }
            
            return Ok(response);
        }
        [HttpGet]
        public IActionResult GetVehicalByUserID(int UserID) {
            if (UserID == null  || UserID==0) {
                return NotFound("Please Check Credentials!");
            }
            var response = _vehicalreg.GetAVehicalByUserID(UserID);
            try
            {
                if (response.UserID == -999)
                {
                    return NotFound("Internal Error Occured!");
                }
                return Ok(response);
            }
            catch (Exception e) {
                return NotFound(e.Message);
            }
            
        }
        [HttpPut]
        public IActionResult UpdateUserAtViewPage(VehicalRegistrationModel data) {
            var response = _vehicalreg.UpdateVehicalFromViewVehicalPage(data);
            try
            {
                if (response == false)
                {
                    return NotFound("Some Server Error Occured!");
                }
                return Ok("Data updated Succesfully");
            }
            catch (Exception e) {
                return NotFound(e.Message);
            }
        }
        [HttpGet("/AllData")]
        public IActionResult GetVehicals()
        {
            var response = _vehicalreg.GetAlldataFromTable;
            try
            {
                if (response == null)
                {
                    return NotFound("Internal Error Occured!");
                }
                return Ok(response);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }
        [HttpDelete]
        public IActionResult DeltevehicalByVehicalID(int vehicalID)
        {
            try
            {
                var response = _vehicalreg.DeleteDataWithVehicalID(vehicalID);
                if (response == -999) {
                    return NotFound("Not FOund Data!");
                }
                return Ok();
            }
            catch (Exception e) {
                return NotFound(e.Message);
            }
        }

    }
}
