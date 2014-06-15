using System;
using StateMachine.Framework;

namespace StateMachine
{
    public class EditMessageAction : StateAction
    {
        private readonly Action<string> _setCurrentMessage;

        public EditMessageAction(Action<string> setCurrentMessage)
        {
            _setCurrentMessage = setCurrentMessage;
        }

        public void SetMessage(string message)
        {
            _setCurrentMessage.Invoke(message);
        }
    }
}