using System.Collections.Generic;
using StateMachine.Framework;
using StateMachine.WorkflowEvents;
using StateMachine.WorkflowStates;

namespace StateMachine.Workflows
{
    public class AccountWorkflow : Workflow
    {
        private WorkflowState _currentState;
        private bool _isAuthenticated; 

        public IEnumerable<StateAction> GetActions()
        {
            if (_isAuthenticated)
                _currentState = new UserLoggedInState(this, new MessageWorkflowState());
            else
                _currentState = new LoggedOutState(this);

            return _currentState.GetActions();
        }
        
        public override void HandleEvent(WorkflowEvent workflowEvent)
        {
            if ((workflowEvent.GetType() == typeof(UserLoggedIn))
                || (workflowEvent.GetType() == typeof(AdminLoggedIn)))
                _isAuthenticated = true;
            if (workflowEvent.GetType() == typeof(LoggedOut))
                _isAuthenticated = false;
                
        }
    }
}