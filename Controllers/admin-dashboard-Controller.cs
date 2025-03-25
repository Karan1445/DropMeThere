using dropmethereapi.Models;
using dropmethereapi.Repos.admin_dashboard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dropmethereapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class admin_dashboard_Controller : ControllerBase
    {
        private readonly AdminUserRepo adminUserRepo;
        public admin_dashboard_Controller(AdminUserRepo adminUserRepo)
        {
            this.adminUserRepo = adminUserRepo;
        }
        [HttpGet]
        public IActionResult GetAllAdmins() {
            try
            {
                var response = adminUserRepo.GetAllAdminUsers();
                if (response == null || response.Count == 0)
                {
                    return NotFound();
                }

                return Ok(response);

            }
            catch (Exception e)
            {
                Console.Write(e);
                return NotFound(new { ErrorEventArgs = "Serevr Side Error!", error = e.Message });
            }
        }
        [HttpGet("/GetAperticularadminbyid")]
        public IActionResult GetAdminByID(int adminID) {
            try
            {
                var response = adminUserRepo.GetAdminUserById(adminID);
                if (response == null)
                {
                    return NotFound();
                }

                return Ok(response);

            }
            catch (Exception e)
            {
                Console.Write(e);
                return NotFound(new { ErrorEventArgs = "Serevr Side Error!", error = e.Message });
            }
        }
        [HttpDelete]
        public IActionResult DeleteAdminUSer(int adminID) {
            try
            {
                var response = adminUserRepo.DeleteAdminUser(adminID);
                if (response == false )
                {
                    return NotFound();
                }

                return Ok(response);

            }
            catch (Exception e)
            {
                Console.Write(e);
                return NotFound(new { ErrorEventArgs = "Serevr Side Error!", error = e.Message });
            }
        }
        [HttpPut]
        public IActionResult UpdateUSer(AdminUserModel asm) {
            try
            {
                var response = adminUserRepo.UpdateAdminUser(asm);
                if (response == false)
                {
                    return NotFound();
                }

                return Ok(response);

            }
            catch (Exception e)
            {
                Console.Write(e);
                return NotFound(new { ErrorEventArgs = "Serevr Side Error!", error = e.Message });
            }
        }
        [HttpPost]
        public IActionResult PostAdminNewUser(AdminUserModel asm) {
            try
            {
                var response = adminUserRepo.InsertAdminUser(asm);
                if (response == false)
                {
                    return NotFound();
                }

                return Ok(response);

            }
            catch (Exception e)
            {
                Console.Write(e);
                return NotFound(new { ErrorEventArgs = "Serevr Side Error!", error = e.Message });
            }
        }
        [HttpGet("/dashboardData")]
        public IActionResult dashboardNumbers() {
           
                var response = adminUserRepo.GetDashbordNumber();
                if (response == null)
                {
                    return NotFound();
                }

                return Ok(response);

            }
           
    }

}
