using StateMachine.Framework;
using StateMachine.WorkflowEvents;

namespace StateMachine.WorkflowActions
{
    public class LoginAction : StateAction
    {
        private readonly Workflow _workflowEventSink;

        public LoginAction(Workflow workflowEventSink)
        {
            _workflowEventSink = workflowEventSink;
        }

        public SessionId Login(LoginCredentials loginCredentials)
        {
            if (loginCredentials.UserName == "Administrator")
            {
                _workflowEventSink.HandleEvent(new AdminLoggedIn());
                return new SessionId();
            }
            if (loginCredentials.Password != string.Empty)
            {
                _workflowEventSink.HandleEvent(new UserLoggedIn());
                return new SessionId();
            }
            _workflowEventSink.HandleEvent(new LoggedOut());
            return new NullSession();
        }
    }
}