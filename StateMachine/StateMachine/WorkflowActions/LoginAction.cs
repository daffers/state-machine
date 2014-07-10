using StateMachine.Framework;
using StateMachine.WorkflowEvents;

namespace StateMachine.WorkflowActions
{
    public class LoginAction : WorkflowAction<LoginCredentials, SessionId>
    {
        private readonly Workflow _workflowEventSink;

        public LoginAction(Workflow workflowEventSink)
        {
            _workflowEventSink = workflowEventSink;
        }

        protected override SessionId Execute(LoginCredentials loginCredentials)
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