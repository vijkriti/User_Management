using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Context.Entities;
using DataContext.Models;

namespace RepositoryInterfaces.Login
{
    public interface ILoginRepository
    {
        KritiUser? AuthenticateUser(LoginDto loginDto);
    }
}
