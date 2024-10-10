using EmployeeAPI.Model.UserDomain;
using EmployeeAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authservices;
        
        public AuthController(IAuthServices authservices)
        {
            _authservices = authservices;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<LoginRespon> Login([FromBody] LoginModel model)
        {

            return await _authservices.LogIn(model);
        }
        [HttpPost]
        [Route("RegisterUser")]
        public async Task<IdentityResult> RegisterUser([FromBody] RegisterModel model)
        {

            return await _authservices.RegisterUser(model);
        }

        [HttpPost]
        [Route("RegisterAdmin")]
        public async Task<IdentityResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            return await _authservices.RegisterAdmin(model);
        }
        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel model)
        {
            var loginresult = await _authservices.RefreshToken(model);
            if (loginresult.IsLoggin)
            {
                return Ok(loginresult);

            }
            return Unauthorized(loginresult);
        }
    }
}

