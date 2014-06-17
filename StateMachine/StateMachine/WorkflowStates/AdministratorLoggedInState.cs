using System.Collections.Generic;
using StateMachine.Framework;
using StateMachine.WorkflowActions;

namespace StateMachine.WorkflowStates
{
    public class AdministratorLoggedInState : WorkflowState
    {
        private readonly Workflow _workflow;
        private readonly MessageWorkflowState _state;

        public AdministratorLoggedInState(Workflow workflow, MessageWorkflowState state)
        {
            _workflow = workflow;
            _state = state;
        }

        public override IEnumerable<StateAction> GetActions()
        {
            return new List<StateAction>()
            {
                new LogoutAction(_workflow),
                new EditMessageAction(_state),
                new ViewMessageAction(_state)
            };
        }
    }
}