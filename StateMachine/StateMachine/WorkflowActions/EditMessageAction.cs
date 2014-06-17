using StateMachine.Framework;

namespace StateMachine.WorkflowActions
{
    public class EditMessageAction : StateAction
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