using StateMachine.Framework;

namespace StateMachine
{
    public class ViewMessageAction : StateAction
    {
        private readonly string _currentMessage;

        public ViewMessageAction(string currentMessage)
        {
            _currentMessage = currentMessage;
        }

        public string GetMessage()
        {
            return _currentMessage;
        }
    }
}