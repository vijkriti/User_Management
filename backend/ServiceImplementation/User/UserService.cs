using DataContext.Models;
using ServiceInterfaces.Email;
using ServiceInterfaces.User;
using RepositoryInterfaces.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DataContext.Entities;

namespace ServiceImplementation.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public UserService(IUserRepository userRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task<IEnumerable<KritiUser>> GetUsersAsync()
        {
            return await _userRepository.GetUsersAsync();
        }

        public async Task<KritiUser> GetUsersByIdAsync(int id)
        {
            return await _userRepository.GetUsersByIdAsync(id);
        }

        public async Task<KritiUser> AddUsersAsync(KritiUser user, IFormFile file)
        {
            // Generate a random password and set the user as inactive
            var password = GenerateRandomPassword();
            user.Password = password;
            user.IsActive = false;

            // Save the image
            if (file != null)
            {
                user.ImgPath = await SaveImageAsync(file);
            }

            // Add the user
            var createdUser = await _userRepository.AddUsersAsync(user);

            // Send activation email
            var activationLink = GenerateActivationLink(createdUser.UserId);
            var emailDto = new EmailDto(
                user.Email,
                "Activate Your Account",
                $"Hello {user.FirstName},<br><br>Your account has been created successfully. Please activate your account by clicking the following link:<br><a href='{activationLink}'>Activate Account</a><br><br>Best Regards,<br>Team"
            );
            _emailService.SendEmail(emailDto);

            return createdUser;
        }

        public async Task UpdateUsersAsync(KritiUser user, IFormFile file)
        {
            // Save the image if a new file is provided
            if (file != null)
            {
                user.ImgPath = await SaveImageAsync(file);
            }

            await _userRepository.UpdateUsersAsync(user);
        }

        public async Task DeleteUsersAsync(int id)
        {
            await _userRepository.DeleteUsersAsync(id);
        }

        public async Task<bool> ActivateUserAsync(int userId)
        {
            var user = await _userRepository.GetUsersByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            if (user.IsActive ?? false)
            {
                return false;
            }

            user.IsActive = true;
            await _userRepository.UpdateUsersAsync(user);
            return true;
        }

        private async Task<string> SaveImageAsync(IFormFile file)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/images/{file.FileName}";
        }

        private string GenerateRandomPassword(int length = 12)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string GenerateActivationLink(int userId)
        {
            // Generate the activation link
            return $"https://localhost:7243/api/User/activate?userId={userId}";
        }
    }
}
