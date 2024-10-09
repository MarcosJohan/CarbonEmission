namespace Api.Services;

public interface IAuthService
{
    Object GetToken(string username, string password);
}