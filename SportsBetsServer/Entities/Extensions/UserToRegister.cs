namespace SportsBetsServer.Entities.Extensions
{
    public class UserToRegister
    {
        private string _username;
        private string _password;
        public UserToRegister()
        {

        }
        public string Username { 
            get
            {
                return _username ?? "";
            } 
            set
            {
                _username = value;
            }
        }
        public string Password { 
            get
            {
                return _password ?? "";
            } 
            set
            {
                _password = value;
            }
        }
    }
}
