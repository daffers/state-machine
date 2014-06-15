using System;
using System.Collections.Generic;
using StateMachine.Framework;

namespace StateMachine
{
    public class UserLoggedInState : WorkflowState
    {
        private readonly Action _hasLoggedOut;
        private readonly string _currentMessage;

        public UserLoggedInState(Action hasLoggedOut, string currentMessage)
        {
            _hasLoggedOut = hasLoggedOut;
            _currentMessage = currentMessage;
        }

        public override IEnumerable<StateAction> GetActions()
        {
            return new List<StateAction>()
            {
                new LogoutAction(_hasLoggedOut),
                new ViewMessageAction(_currentMessage)
            };
        }
    }
}