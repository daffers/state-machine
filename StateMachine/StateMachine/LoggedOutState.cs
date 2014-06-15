using System;
using System.Collections.Generic;
using StateMachine.Framework;

namespace StateMachine
{
    public class LoggedOutState : WorkflowState
    {
        private readonly Action _adminLogin;
        private readonly AccountWorkflow _accountWorkflow;
        private readonly Action _usedLogin;

        public LoggedOutState(Action usedLogin, Action adminLogin, AccountWorkflow accountWorkflow)
        {
            _adminLogin = adminLogin;
            _accountWorkflow = accountWorkflow;
            _usedLogin = usedLogin;
        }

        public override IEnumerable<StateAction> GetActions()
        {
            return new List<LoginAction>()
            {
                new LoginAction(_usedLogin, _adminLogin, _accountWorkflow)
            };
        }


    }
}