using StateMachine.Framework;

namespace StateMachine.ExampleWorkflows.WorkflowActions
{
    public class ViewMessageAction : WorkflowAction
    {
        private readonly MessageWorkflowState _state;

        public ViewMessageAction(object state)
        {
            _state = (MessageWorkflowState)state;
        }
        
        public string GetMessage()
        {
            return _state.CurrentMessage;
        }
    }
}