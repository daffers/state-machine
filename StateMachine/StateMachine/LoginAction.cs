using System;
using StateMachine.Framework;

namespace StateMachine
{
    public class LoginAction : StateAction
    {
        private readonly Action _userLoggedIn;
        private readonly Action _adminLogin;
        private readonly AccountWorkflow _workflowEventSink;

        public LoginAction(Action userLoggedIn, Action adminLogin, AccountWorkflow workflowEventSink)
        {
            _userLoggedIn = userLoggedIn;
            _adminLogin = adminLogin;
            _workflowEventSink = workflowEventSink;
        }

        public SessionId Login(LoginCredentials loginCredentials)
        {
            if (loginCredentials.UserName == "Administrator")
            {
                _adminLogin.Invoke();
                _workflowEventSink.HandleEvent(new AdminLoggedIn());
            }
            if (loginCredentials.Password == string.Empty)
                return new NullSession();

            _workflowEventSink.HandleEvent(new UserLoggedIn());
            _userLoggedIn.Invoke();
            return new SessionId();
        }
    }

    public class UserLoggedIn : WorkflowEvent { }
    public class AdminLoggedIn : WorkflowEvent { }
}