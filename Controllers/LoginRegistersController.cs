using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using dropmethereapi.Repos.UserLogin;
using dropmethereapi.Models;
using System.Text.Json;
using FluentValidation.Validators;
using System.Data.SqlClient;
namespace dropmethereapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginRegistersController : ControllerBase
    {   
        private readonly IConfiguration _configuration;
        private readonly LoginRegistrations _loginRegistrations; 

        public LoginRegistersController(IConfiguration configuration,LoginRegistrations lr) {
            _configuration = configuration;
            _loginRegistrations = lr;
        }
        [HttpPost]
        public IActionResult Login(userDumomodel AuthenticationData)
        {
            User userData = _loginRegistrations.LoginIntoUserAccount(AuthenticationData.Email,AuthenticationData.PassWord);
            if (userData.UserName.Equals(_configuration["LoginMetaData:UserNamePharse"]))
            {
                return NotFound("Please Validate Email and PassWord");
            }
          
            //--DemoCode-NoProd
            //CreateClaims-s-1
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub,_configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //Here Give User ID from User Object
                new Claim("UserID",Convert.ToString(userData.UserID)),
                new Claim("UserEmail",userData.Email),
            };
            //Get the key From config
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            //signin object
            var signIn=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            //generate token
            var jwtToken = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires:DateTime.UtcNow.AddMinutes(360),
                signingCredentials:signIn
                );
            //--token-over
            string tokenValue=new JwtSecurityTokenHandler().WriteToken(jwtToken);
            //--overclaimns
            //--DemoCode-NoProd
            return Ok(new {Token=tokenValue,User=userData});
            //return View();
        }

        [HttpPut]
        public IActionResult ForgetPass(ForgetModel frg) {
            try
            {
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ConnectionString"));
                con.Open();
                SqlCommand cmd=con.CreateCommand();
                cmd.CommandText = "forget_password_pr";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", frg.Email);
                cmd.Parameters.AddWithValue("@PassWord", frg.Password);
                cmd.Parameters.AddWithValue("@PhoneNumber ", frg.PhoneNumber);
                int k=cmd.ExecuteNonQuery();
                return Ok("Try Now!");
            }
            catch (Exception e) {
                return NotFound(e.Message);
            }
          
        }
        [HttpGet]
        [Authorize]
        public IActionResult GetDataFromToken() {
            var token =HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase); ;
            var handler = new JwtSecurityTokenHandler();
            if (handler.CanReadToken(token))
            {
                var jwtToken = handler.ReadJwtToken(token);

                // Print Header
                Console.WriteLine("Header:");
                foreach (var header in jwtToken.Header)
                {
                    Console.WriteLine($"  {header.Key}: {header.Value}");
                }

                // Print Payload (Claims)
                Console.WriteLine("\nPayload:");
                foreach (var claim in jwtToken.Claims)
                {
                    Console.WriteLine($"  {claim.Type}: {claim.Value}");
                }
            }
            else
            {
                Console.WriteLine("Invalid JWT Token.");
            }
        
            return Ok("Hiii you are there");
        }
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser(userRegsitermodel userData)
        {
            var responseData = _loginRegistrations.RegisterForNewUser(userData);
            if (responseData.UserName.Equals(_configuration["LoginMetaData:UserRegisterationError"]))
            {
                return NotFound("Uh...User is Not registerd Try it again after sometime");
            }
            if (responseData.UserName.Equals(_configuration["LoginMetaData:SameUserEmailError"]))
            {
                return NotFound("Uh...User Email is alredy Registerd! try with new Email");
            }
            if (responseData.IsDriver.Equals(_configuration["LoginMetaData:UserRegisterationError"])) {
                return NotFound(responseData.UserName);
            }
            try
            {
                userDumomodel inputData = new userDumomodel(){ Email = userData.Email, PassWord = userData.PassWord };

                IActionResult  loginResponse = Login(inputData);


                if (loginResponse is OkObjectResult okResult)
                {
                    var response = okResult.Value as dynamic;

                    string token = response?.Token;
                    User user = response?.User;

                    // Return the token and user data
                    return Ok(new { Token = token, User = user });
                }
                else
                {
                    // Handle other possible statuses (e.g., error handling)
                    return NotFound("Login failed.");
                }
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }


    }
}
