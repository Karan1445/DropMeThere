using dropmethereapi.Models;
using dropmethereapi.Repos.SeekerRequestHandler;
using dropmethereapi.Repos.UserFunctions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dropmethereapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SeekerRequestHandlerController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SeekerRequestHandler _srh;
        
        public SeekerRequestHandlerController(IConfiguration icn,SeekerRequestHandler srh) {
            _configuration = icn;
            _srh = srh;
        }
        [HttpPost]
        public IActionResult MakeANewRequst_Insert(SeekerRequestHandlerModel data) {
            var resposne = _srh.MakeANewRequest_Inset(data);
            if (resposne == false)
            {
                return StatusCode(500, "Server Side Error");
            }
            else {
                return Ok();
            }
        }
        [HttpPut]
        public IActionResult UpdateRequestByRequestID(SeekerRequestHandlerModel data)
        {
            var resposne = _srh.UpdateRequetByRequestID(data);
            if (resposne == false)
            {
                return StatusCode(500, "Server Side Error");
            }
            else
            {
                return Ok();
            }
        }
        [HttpGet("GetWithSpecificUser")]
        public IActionResult GetWithSpecificUserID(int UserID) {
            try
            {
                var response = _srh.GetAllRequestOfPerticularUser(UserID);
                if (response == null)
                {
                    response = [];
                    return Ok(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                return StatusCode(500, "ServerSideError");
                
            }
        }
        [HttpDelete]
        public IActionResult DeleteRequestWithRequestID(int RequestID)
        {
            var resposne = _srh.DeleteRequestByRequestID(RequestID);
            if (resposne == false)
            {
                return StatusCode(500, "Server Side Error");
            }
            else
            {
                return Ok();
            }
        }
        [HttpGet("GetWithSpecificRequest")]
        public IActionResult GetARequestWithID(int RequestID)
        {
            try
            {
                var response = _srh.GetARequestByID(RequestID);
                if (response == null)
                {
                    response = new SeekerRequestHandlerModel();
                    return Ok(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, "ServerSideError");

            }
        }
        [HttpGet("AllTableData")]
        public IActionResult GetAllData() {
            try
            {
                var response = _srh.GetAllData();
                if (response == null)
                {
                    response = [];
                    return Ok(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, "ServerSideError");

            }
        }


    }
}
