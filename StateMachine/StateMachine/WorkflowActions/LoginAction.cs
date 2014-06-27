using StateMachine.Framework;
using StateMachine.WorkflowEvents;

namespace StateMachine.WorkflowActions
{
    public class LoginAction : WorkflowActionTyped<LoginCredentials, SessionId>
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

        protected override SessionId Execute(LoginCredentials input)
        {
            if (input.UserName == "Administrator")
            {
                _workflowEventSink.HandleEvent(new AdminLoggedIn());
                return new SessionId();
            }
            if (input.Password != string.Empty)
            {
                _workflowEventSink.HandleEvent(new UserLoggedIn());
                return new SessionId();
            }
            _workflowEventSink.HandleEvent(new LoggedOut());
            return new NullSession();
        }

        public override object ExcuteAction(object input)
        {
            var loginCredentials = input as LoginCredentials;
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