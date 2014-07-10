using StateMachine.Framework;

namespace StateMachine.WorkflowActions
{
    public class ViewMessageAction : WorkflowAction
    {
        private readonly MessageWorkflowState _state;

        public ViewMessageAction(MessageWorkflowState state)
        {
            _state = state;
        }
        
        public override object Execute(object input)
        {
            return _state.CurrentMessage;
        }
    }
}