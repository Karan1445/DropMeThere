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
   // [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly Users u;
        public UsersController(Users users)
        {
            u = users;
        }

        [HttpGet("/GetAll")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var response = u.GetAllUsers();
                if (response == null || response.Count==0) {
                    return NotFound();
                }
               
                   return Ok(response);
                
            }
            catch (Exception e) {
                Console.Write(e);
            return NotFound(new {ErrorEventArgs= "Serevr Side Error!",error=e.Message});
            }
        }
        [HttpGet("/GetAUser")]
        public IActionResult GetAPerticularUser(int UserID)
        {
            try
            {
                var response = u.GetUserById(UserID);
                if (response == null)
                {
                    return NotFound();
                }

                return Ok(response);

            }
            catch (Exception e)
            {
                return NotFound(new { ErrorEventArgs = "Serevr Side Error!", error = e.Message });
            }
        }
        [HttpPut]
        public IActionResult UpdateUser(User user) {
            try
            {
                var response = u.UpdateUser(user);
                if (response ==false)
                {
                    return NotFound();
                }

                return Ok(response);

            }
            catch (Exception e)
            {
                return NotFound(new { ErrorEventArgs = "Serevr Side Error!", error = e.Message });
            }
        }
        [HttpDelete]
        public IActionResult DelteUser(int UserID) {
            try
            {
                var response = u.DeleteUser(UserID);
                if (response == false)
                {
                    return NotFound();
                }

                return Ok(response);

            }
            catch (Exception e)
            {
                return NotFound(new { ErrorEventArgs = "Serevr Side Error!", error = e.Message });
            }
        }
    }
}
