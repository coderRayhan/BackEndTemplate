using Application.Common.Models;

namespace Application.Common.Interfaces;
public interface IAuthService
{
    Task<(bool isSuccess, string result)> Login(string username, string password);
}
