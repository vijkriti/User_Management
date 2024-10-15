using DataContext.Data;
using DataContext.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryInterfaces.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryImplementation.User
{
    public class UserRepository : IUserRepository
    {
        public readonly SDirectContext _context;

        public UserRepository(SDirectContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<KritiUser>> GetUsersAsync()
        {
            return await _context.KritiUsers.ToListAsync();
        }
        public async Task<KritiUser> GetUsersByIdAsync(int id)
        {
            return await _context.KritiUsers.FindAsync(id);
        }
        public async Task<KritiUser> AddUsersAsync(KritiUser user)
        {
            _context.KritiUsers.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task UpdateUsersAsync(KritiUser user)
        {
            _context.KritiUsers.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteUsersAsync(int id)
        {
            var user = await _context.KritiUsers.FindAsync(id);
            if (user != null)
            {
                _context.KritiUsers.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
