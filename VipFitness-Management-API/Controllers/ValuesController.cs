using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VipFitness_Management_API.Entities;

namespace VipFitness_Management_API.Controllers
{
    [Route("Authenticator")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        UserEntity userEntity = new UserEntity();
        [HttpPost]
        public Task<IActionResult> AuthenticateAsync([FromBody] AuthenticationRequest request)
        {

            var isAuthenticated = userEntity.UserCheck(request.Username, request.Password);
            if (isAuthenticated == 0)
                return Task.FromResult<IActionResult>(Unauthorized(new { Message = "Giriş Yapılamadı", Success = false }));
            else
                return Task.FromResult<IActionResult>(Ok(new { Message = "Giriş Başarılı", Success = true }));

        }


        public class AuthenticationRequest
        {
            public string? Username { get; set; }
            public string? Password { get; set; }
        }

    }
}
