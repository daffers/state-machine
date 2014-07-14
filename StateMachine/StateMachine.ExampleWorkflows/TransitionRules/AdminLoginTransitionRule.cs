using StateMachine.ExampleWorkflows.WorkflowEvents;
using StateMachine.ExampleWorkflows.WorkflowStates;
using StateMachine.Framework;

namespace StateMachine.ExampleWorkflows.TransitionRules
{
    public class AdminLoginTransitionRule : TransitionRule
    {
        public override WorkflowState Transition(IWorkflowEvent workflowEvent, Workflow workflow, object state)
        {
            return workflowEvent.GetType() == typeof(AdminLoggedIn) ? new AdministratorLoggedInState(workflow, state) : null;
        }
    }
}