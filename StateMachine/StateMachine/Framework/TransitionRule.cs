namespace StateMachine.Framework
{
    public abstract class TransitionRule
    {
        public abstract WorkflowState Transition(WorkflowEvent workflowEvent, Workflow workflow, MessageWorkflowState state);
    }
}