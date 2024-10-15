using Microsoft.AspNetCore.Mvc;
using ServiceInterfaces.Login;
using Data_Context.Entities;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController: ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("authenticate")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var token = _loginService.AuthenticateUser(loginDto);
                if (token == null)
                {
                    return Unauthorized("Invalid email or password.");
                }

                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during login: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
