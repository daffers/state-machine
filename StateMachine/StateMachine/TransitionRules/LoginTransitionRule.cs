using StateMachine.Framework;
using StateMachine.WorkflowEvents;
using StateMachine.WorkflowStates;

namespace StateMachine.TransitionRules
{
    public class LoginTransitionRule : TransitionRule
    {
        public override WorkflowState Transition(IWorkflowEvent workflowEvent, Workflow workflow, MessageWorkflowState state)
        {
            return (workflowEvent.GetType() == typeof(UserLoggedIn)) ? new UserLoggedInState(workflow, state) : null;
        }
    }
}