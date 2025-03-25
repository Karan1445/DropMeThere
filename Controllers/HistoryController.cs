using dropmethereapi.Repos.History;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace dropmethereapi.Controllers
{
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class HistoryController : ControllerBase
    {
        private readonly HistoryAll his;
        private readonly IConfiguration icn;
        public HistoryController(HistoryAll his1,IConfiguration icn1) {
            icn = icn1;
            his = his1;
        }
        [HttpDelete]
        public IActionResult DeleteAdnSaveDate(int id)
        {
            try
            {
                var resposne = his.DeleteAndStoreRecord(id);
                if (resposne == "nO--data-found") {
                    return NotFound("No Data FOund");
                }
                if (resposne == "Yes") {
                    return Ok();
                }
                Console.Write(resposne);
                return NotFound("Server Side Error");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server Side error");
            }
        }
        [HttpGet]
        public IActionResult GetAllHistory(int UserID) {
            try
            {
                var response = his.GetAllRideHistoryRequests(UserID);
                if (response == null) {
                    return NotFound();
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(501, "Server Side Error!");
            }

        }
        [HttpPut]
        public IActionResult UpdateGift(int id, string rrfd, string HelpFromSeeker)
        {
            try {
                var response = his.sendGift(id, rrfd, HelpFromSeeker);
                if (response == "9999") {
                    return NotFound();
                }
                if (response == "No Data") {
                    return NotFound();
                }
                return Ok();
            }catch(Exception e){
                return StatusCode(501, "Server Side Error!");
            }
        }
        [HttpGet("/GetAllDataadmin")]
        public IActionResult GetALlDataadmin() {
            try
            {
                var response = his.GetAlldataAdmin();
                if (response == null)
                {
                    return NotFound();
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(501, "Server Side Error!");
            }
        }
        [HttpDelete("/adminside")]
        public IActionResult DeleteByID(int id)
        {
            try
            {
                var resposne = his.delteHistoryByID(id);
                if (resposne == "nO--data-found")
                {
                    return NotFound("No Data FOund");
                }
                if (resposne == "Yes")
                {
                    return Ok();
                }
                Console.Write(resposne);
                return NotFound("Server Side Error");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server Side error");
            }
        }
    }
}
