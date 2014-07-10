using System;
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

        public override IEnumerable<WorkflowAction> GetActions()
        {
            return new List<WorkflowAction>()
            {
                new LogoutAction(_workflow),
                new ViewMessageAction(_state)
            };
        }

        public override IEnumerable<Type> GetAvailableActions()
        {
            return new List<Type>()
            {
                typeof(LogoutAction),
                typeof(ViewMessageAction)
            };
        }
    }
}