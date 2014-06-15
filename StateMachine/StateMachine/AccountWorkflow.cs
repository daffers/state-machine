using System.Collections.Generic;
using StateMachine.Framework;

namespace StateMachine
{
    public class AccountWorkflow
    {
        private WorkflowState _currentState;
        private bool _isAuthenticated; 

        public IEnumerable<StateAction> GetActions()
        {
            if (_isAuthenticated)
                _currentState = new UserLoggedInState(HasLoggedOut, null);
            else
                _currentState = new LoggedOutState(HasLoggedIn, AdminLogin, this);

            return _currentState.GetActions();
        }

        private void HasLoggedIn()
        {
            _isAuthenticated = true;
        }
        
        private void AdminLogin()
        {
        }

        private void HasLoggedOut()
        {
            _isAuthenticated = false;
        }

        public void HandleEvent(WorkflowEvent workflowEvent)
        {
            if (workflowEvent.GetType() == typeof())
        }
    }

    public class WorkflowEvent
    {
    }
}