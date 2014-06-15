using System;
using StateMachine.Framework;

namespace StateMachine
{
    public class LogoutAction : StateAction
    {
        private readonly Action _hasLoggedOut;

        public LogoutAction(Action hasLoggedOut)
        {
            _hasLoggedOut = hasLoggedOut;
        }

        public LogoutAction(LoggedOutEventHandler handler)
        {
   
        }

        public void Logout()
        {
            _hasLoggedOut.Invoke();
        }

        public event LoggedOutEventHandler UserLoggedOut;
    }

    public delegate void LoggedOutEventHandler();
}