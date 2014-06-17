using System.Collections.Generic;
using StateMachine.Framework;
using StateMachine.WorkflowEvents;
using StateMachine.WorkflowStates;

namespace StateMachine.Workflows
{
    public class MessageWorkflow : Workflow
    {
        internal enum AuthenticationStatus
        {
            NotAuthenticated,
            AuthenticatedAsAdmin,
            AuthenticatedAsUser
        }

        private WorkflowState _currentState;
        private readonly MessageWorkflowState _state = new MessageWorkflowState() {CurrentMessage = string.Empty};
        private AuthenticationStatus _authenticationState;

        public IEnumerable<StateAction> GetActions()
        {
            if (_authenticationState == AuthenticationStatus.AuthenticatedAsUser)
                _currentState = new UserLoggedInState(this, _state);
            else if (_authenticationState == AuthenticationStatus.AuthenticatedAsAdmin)
                _currentState = new AdministratorLoggedInState(this, _state);
            else
                _currentState = new LoggedOutState(this);

            return _currentState.GetActions();
        }

        public override void HandleEvent(WorkflowEvent workflowEvent)
        {
            if (workflowEvent.GetType() == typeof(UserLoggedIn))
                _authenticationState = AuthenticationStatus.AuthenticatedAsUser;
            if (workflowEvent.GetType() == typeof(AdminLoggedIn))
                _authenticationState = AuthenticationStatus.AuthenticatedAsAdmin;
            if (workflowEvent.GetType() == typeof(LoggedOut))
                _authenticationState = AuthenticationStatus.NotAuthenticated;
        }
    }
}