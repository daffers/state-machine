using System;
using System.Collections.Generic;
using StateMachine.Framework;

namespace StateMachine
{
    public class AdministratorLoggedInState : WorkflowState
    {
        private readonly Action _hasLoggedOut;
        private readonly string _currentMessage;
        private readonly Action<string> _setCurrentMessage;

        public AdministratorLoggedInState(Action hasLoggedOut, string currentMessage, Action<string> setCurrentMessage)
        {
            _hasLoggedOut = hasLoggedOut;
            _currentMessage = currentMessage;
            _setCurrentMessage = setCurrentMessage;
        }

        public override IEnumerable<StateAction> GetActions()
        {
            return new List<StateAction>()
            {
                new LogoutAction(_hasLoggedOut),
                new EditMessageAction(_setCurrentMessage),
                new ViewMessageAction(_currentMessage)
            };
        }
    }
}