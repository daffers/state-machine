using System.Collections.Generic;
using StateMachine.Framework;
using StateMachine.WorkflowActions;

namespace StateMachine.WorkflowStates
{
    public class LoggedOutState : WorkflowState
    {
        private readonly Workflow _workflow;

        public LoggedOutState(Workflow workflow)
        {
            _workflow = workflow;
        }

        public override IEnumerable<StateAction> GetActions()
        {
            return new List<LoginAction>()
            {
                new LoginAction(_workflow)
            };
        }
    }
}