namespace SportsBetsServer.Contracts.Services
{
    public interface IUserService
    {
        bool UserExists(string username);
    }
}