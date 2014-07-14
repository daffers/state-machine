using System.Collections.Generic;
using StateMachine.ExampleWorkflows.WorkflowActions;
using StateMachine.Framework;

namespace StateMachine.ExampleWorkflows.WorkflowStates
{
    public class UserLoggedInState : WorkflowState
    {
        private readonly Workflow _workflow;
        private readonly object _state;

        public UserLoggedInState(Workflow workflow, object state)
        {
            _workflow = workflow;
            _state = state;
        }

        public override IEnumerable<WorkflowAction> GetActions()
        {
            return new List<WorkflowAction>()
            {
                new LogoutAction(_workflow),
                new ViewMessageAction(_state)
            };
        }
    }
}