using StateMachine.Framework;
using StateMachine.WorkflowEvents;
using StateMachine.WorkflowStates;

namespace StateMachine.TransitionRules
{
    public class LogoutTransitionRule : TransitionRule
    {
        public override WorkflowState Transition(WorkflowEvent workflowEvent, Workflow workflow, MessageWorkflowState state)
        {
            return workflowEvent.GetType() == typeof(LoggedOut) ? new LoggedOutState(workflow) : null;
        }
    }
}