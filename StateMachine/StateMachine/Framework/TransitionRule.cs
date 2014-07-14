namespace StateMachine.Framework
{
    public abstract class TransitionRule
    {
        public abstract WorkflowState Transition(IWorkflowEvent workflowEvent, Workflow workflow, object state);
    }
}