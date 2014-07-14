using System.Collections.Generic;
using StateMachine.ExampleWorkflows.WorkflowActions;
using StateMachine.Framework;

namespace StateMachine.ExampleWorkflows.WorkflowStates
{
    public class LoggedOutState : WorkflowState
    {
        private readonly Workflow _workflow;

        public LoggedOutState(Workflow workflow)
        {
            _workflow = workflow;
        }

        public override IEnumerable<WorkflowAction> GetActions()
        {
            return new List<LoginAction>()
            {
                new LoginAction(_workflow)
            };
        }
    }
}