using DataContext.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceInterfaces.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KritiUser>>> GetUsersAsync()
        {
            try
            {
                var users = await _userService.GetUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching users: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<KritiUser>> GetUsersByIdAsync(int id)
        {
            try
            {
                var user = await _userService.GetUsersByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching user by ID: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<KritiUser>> AddUsersAsync([FromForm] string user, [FromForm] IFormFile file)
        {
            if (string.IsNullOrEmpty(user))
            {
                return BadRequest("User information is null.");
            }

            var userObj = Newtonsoft.Json.JsonConvert.DeserializeObject<KritiUser>(user);
            if (userObj == null)
            {
                return BadRequest("Invalid user data");
            }

            try
            {
                var createdUser = await _userService.AddUsersAsync(userObj, file);
                return Ok(createdUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while creating the user: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsersAsync(int id, [FromForm] string user, [FromForm] IFormFile file)
        {
            var userObj = Newtonsoft.Json.JsonConvert.DeserializeObject<KritiUser>(user);
            if (id != userObj.UserId || userObj == null)
            {
                return BadRequest();
            }

            try
            {
                await _userService.UpdateUsersAsync(userObj, file);
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating the user: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsersAsync(int id)
        {
            try
            {
                await _userService.DeleteUsersAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting the user: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("activate")]
        public async Task<IActionResult> ActivateUser(int userId)
        {
            try
            {
                var success = await _userService.ActivateUserAsync(userId);
                if (!success)
                {
                    return BadRequest("User is either not found or already active");
                }

                return Ok("User activated successfully. You can now log in.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while activating the user: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
