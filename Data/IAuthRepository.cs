namespace DotNet_rpg.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string UserName, string password);
        Task<bool> UserExists(string username);
    }
}