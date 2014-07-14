using StateMachine.Framework;

namespace StateMachine.ExampleWorkflows.WorkflowActions
{
    public class EditMessageAction : WorkflowAction
    {
        private readonly MessageWorkflowState _state;

        public EditMessageAction(object state)
        {
            _state = (MessageWorkflowState)state;
        }

        public void SetMessage(string message)
        {
            _state.CurrentMessage = message;
        }
    }
}