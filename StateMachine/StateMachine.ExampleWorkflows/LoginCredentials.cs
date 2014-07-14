namespace StateMachine.ExampleWorkflows
{
    public class LoginCredentials
    {
        public LoginCredentials(string username, string password)
        {
            Password = password;
            UserName = username;
        }

        public string Password { get; private set; }
        public string UserName { get; set; }
    }
}