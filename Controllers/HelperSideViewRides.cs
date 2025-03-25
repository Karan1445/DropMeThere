using dropmethereapi.Models;
using dropmethereapi.Repos.HelperSideViewRides;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace dropmethereapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class HelperSideViewRides : ControllerBase
    {
        private readonly HelperSideViewRidesRepo _hsvrr;

        public HelperSideViewRides(HelperSideViewRidesRepo hvrr) {
            _hsvrr = hvrr;
        }
        [HttpPost]
        public IActionResult InsertANewRideConfimation(HelperSideViewRidesModel data)
        {
            try
            {
                var response = _hsvrr.InsertANewRideConfimation(data);
                if (response) return Ok("Inserted SucessFully");
                else return NotFound("You Are Helping Someone Alredy!");
            }
            catch (Exception e) {
                return NotFound(e.Message);
            }
        }
        [HttpGet]
        public IActionResult DisplayActiveRideForboth(int UserID) {
            try
            {
                var response = _hsvrr.DisplayActiveRideForboth(UserID);
                if (response.HelperUserName == "Not----------------data------------------found----123") {
                    return NotFound("No Active Ride Found For User!");
                }
                if (response.HelperUserName == "sql----------------Error------------------found----123")
                {
                    return NotFound("Server Error !");
                }
                return Ok(response);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpGet("/temporary")]
        public IActionResult TemporaryDisplayreqquest()
        {
            try
            {
                var respose = _hsvrr.GetAllrequestTemporay();
                if (respose != null)
                {
                    return Ok(respose);
                }
                else {
                    return NotFound("No Data Here");
                }
            }
            catch (Exception e) {
                return NotFound(e.Message);
            }
        }
        [HttpGet("/getalldata")]
        public IActionResult GetAlldata()
        {
            try
            {
                var respose = _hsvrr.GetAllData();
                if (respose != null)
                {
                    return Ok(respose);
                }
                else
                {
                    return NotFound("No Data Here");
                }
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpDelete]
        public IActionResult DeleteDataWIthID(int id) {
           
                try
                {
                    var respose = _hsvrr.DelteDataWIthID(id);
                    if (respose != null)
                    {
                        return Ok(respose);
                    }
                    else
                    {
                        return NotFound("No Data Here");
                    }
                }
                catch (Exception e)
                {
                    return NotFound(e.Message);
                }
            }
        
    }
}
