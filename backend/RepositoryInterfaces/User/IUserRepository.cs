using DataContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryInterfaces.User
{
    public interface IUserRepository
    {
        Task<IEnumerable<KritiUser>> GetUsersAsync();
        Task<KritiUser> GetUsersByIdAsync(int id);
        Task<KritiUser> AddUsersAsync(KritiUser book);
        Task UpdateUsersAsync(KritiUser book);
        Task DeleteUsersAsync(int id);

    }
}
