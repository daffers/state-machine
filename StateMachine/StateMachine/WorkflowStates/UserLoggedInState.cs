using System.Collections.Generic;
using StateMachine.Framework;
using StateMachine.WorkflowActions;

namespace StateMachine.WorkflowStates
{
    public class UserLoggedInState : WorkflowState
    {
        private readonly Workflow _workflow;
        private readonly MessageWorkflowState _state;

        public UserLoggedInState(Workflow workflow, MessageWorkflowState state)
        {
            _workflow = workflow;
            _state = state;
        }

        public override IEnumerable<StateAction> GetActions()
        {
            return new List<StateAction>()
            {
                new LogoutAction(_workflow),
                new ViewMessageAction(_state)
            };
        }
    }
}