using StateMachine.Framework;
using StateMachine.WorkflowEvents;
using StateMachine.WorkflowStates;

namespace StateMachine.TransitionRules
{
    public class AdminLoginTransitionRule : TransitionRule
    {
        public override WorkflowState Transition(IWorkflowEvent workflowEvent, Workflow workflow, MessageWorkflowState state)
        {
            return workflowEvent.GetType() == typeof(AdminLoggedIn) ? new AdministratorLoggedInState(workflow, state) : null;
        }
    }
}