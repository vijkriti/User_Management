using Data_Context.Entities;
using DataContext.Models;

namespace ServiceInterfaces.Login
{
    public interface ILoginService
    {
        string AuthenticateUser(LoginDto loginDto);
        string GenerateJwtToken(KritiUser user);
    }
}
