using SportsBetsServer.Helpers;

namespace SportsBetsServer.Services
{
    public class ErrorService
    {
        public void Error()
        {
            throw new AppException("Username or Password is incorrect.");
        }
    }
}
