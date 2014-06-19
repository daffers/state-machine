using StateMachine.Framework;

namespace StateMachine.WorkflowActions
{
    public class EditMessageAction : WorkflowAction
    {
        private readonly MessageWorkflowState _state;

        public EditMessageAction(MessageWorkflowState state)
        {
            _state = state;
        }

        public void SetMessage(string message)
        {
            _state.CurrentMessage = message;
        }
    }
}