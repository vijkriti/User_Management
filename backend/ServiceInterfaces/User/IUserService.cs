using DataContext.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceInterfaces.User
{
    public interface IUserService
    {
        Task<IEnumerable<KritiUser>> GetUsersAsync();
        Task<KritiUser> GetUsersByIdAsync(int id);
        Task<KritiUser> AddUsersAsync(KritiUser user, IFormFile file);
        Task UpdateUsersAsync(KritiUser user, IFormFile file);
        Task DeleteUsersAsync(int id);
        Task<bool> ActivateUserAsync(int userId);
    }
}
