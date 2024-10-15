using DataContext.Models;
using DataContext.Data;
using RepositoryInterfaces.Login;
using Data_Context.Entities;

public class LoginRepository : ILoginRepository
{
    private readonly SDirectContext _context;

    public LoginRepository(SDirectContext context)
    {
        _context = context;
    }

    public KritiUser? AuthenticateUser(LoginDto loginDto)
    {
        return _context.KritiUsers.SingleOrDefault(u => u.Email == loginDto.Email && u.Password == loginDto.Password);
    }
}
