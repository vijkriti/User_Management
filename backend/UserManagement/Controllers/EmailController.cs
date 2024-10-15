using DataContext.Data;
using DataContext.Entities;
using DataContext.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceInterfaces.Email;
using Shared.Templates;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly SDirectContext _context;
        private readonly IConfiguration _config;
        private readonly IEmailService _emailService;

        public EmailController(SDirectContext context, IConfiguration config, IEmailService emailService)
        {
            _context = context;
            _config = config;
            _emailService = emailService;
        }

        [HttpPost("send-reset-email/{email}")]
        public async Task<IActionResult> SendEmail(string email)
        {
            var user = await _context.KritiUsers.FirstOrDefaultAsync(a => a.Email == email);
            if (user == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Email does not exist"
                });
            }

            var tokenBytes = new byte[64];
            new Random().NextBytes(tokenBytes); 
            var emailToken = Convert.ToBase64String(tokenBytes);

            user.ResetPasswordToken = emailToken;
            user.ResetExpiryToken = DateTime.Now.AddMinutes(10);

            string from = _config["EmailSettings:From"];
            var emailDto = new EmailDto(email, "Reset Password", EmailBody.EmailStringBody(email, emailToken));

            _emailService.SendEmail(emailDto);

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "Email Sent"
            });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var newToken = resetPasswordDto.EmailToken.Replace(" ", "+");
            var user = await _context.KritiUsers.FirstOrDefaultAsync(a => a.Email == resetPasswordDto.Email);
            if (user == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "User does not exist"
                });
            }

            var tokenCode = user.ResetPasswordToken;
            var emailTokenExpiry = user.ResetExpiryToken;
            if (tokenCode != resetPasswordDto.EmailToken || emailTokenExpiry < DateTime.Now)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Invalid or expired token"
                });
            }

            user.Password = resetPasswordDto.NewPassword;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "Reset Password done successfully"
            });
        }
    }
}
