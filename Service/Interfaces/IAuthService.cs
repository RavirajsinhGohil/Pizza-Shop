using Repository.Models;

namespace Service.Interfaces;

public interface IAuthService
{
    User Login(string email, string password);
    string GenerateJwtToken(string email, int RoleId);

    Task SendEmailAsync(string email, string subject, string htmlMessage);
}
