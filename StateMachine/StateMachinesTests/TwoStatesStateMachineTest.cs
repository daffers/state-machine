using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace StateMachinesTests
{
    [TestFixture]
    public class TwoStatesStateMachineTest
    {
        [Test]
        public void TheValidActionForLoggedOutStateIsToLogin()
        {
            var loggedOut = new LoggedOutState(null);
            IEnumerable<LoginAction> actions = loggedOut.GetActions();

            Assert.That(actions.Count(), Is.EqualTo(1));
            Assert.That(actions.First(), Is.TypeOf<LoginAction>());
        }

        [Test]
        public void TheValidActionForLoggedInStateIsToLogout()
        {
            var loggedin = new LoggedInState(null);
            IEnumerable<LogoutAction> actions = loggedin.GetActions();

            Assert.That(actions.Count(), Is.EqualTo(1));
            Assert.That(actions.First(), Is.TypeOf<LogoutAction>());
        }

        [Test]
        public void WhenTheWorkFlowIsInitializedTheFirstActionIsLogin()
        {
            var accountWorkflow = new AccountWorkflow();
            IEnumerable<StateAction> actions = accountWorkflow.GetActions();

            Assert.That(actions.Count(), Is.EqualTo(1));
            Assert.That(actions.First(), Is.TypeOf<LoginAction>());
        }

        [Test]
        public void WhenCorrectLoginCredentialsSuppliedResultIsASessionId()
        {
            var loginCredentials = new LoginCredentials("username", "password");
            var loginAction = new LoginAction(new Action(() => {}));
            var result = loginAction.Login(loginCredentials);

            Assert.That(result, Is.TypeOf<SessionId>());
        }

        [Test]
        public void WhenIncorrectLoginDetailsSuppliedResultIsANullSession()
        {
            var loginCredentials = new LoginCredentials("username", "");
            var loginAction = new LoginAction(null);
            var result = loginAction.Login(loginCredentials);

            Assert.That(result, Is.TypeOf<NullSession>());
        }

        [Test]
        public void AfterSuccessfulLoginThroughWorflowActionBecomesLogout()
        {
            var accountWorkflow = new AccountWorkflow();
            IEnumerable<StateAction> actions = accountWorkflow.GetActions();

            LoginAction login = (LoginAction)actions.First();

            login.Login(new LoginCredentials("username", "password"));

            var actionsPostLogin = accountWorkflow.GetActions();

            var postLoginAction = actionsPostLogin.First();

            Assert.That(postLoginAction, Is.TypeOf<LogoutAction>());
        }

        [Test]
        public void WhenLoginUnsuccessfulWorkflowActionRemainsAsLogin()
        {
            var accountWorkflow = new AccountWorkflow();
            IEnumerable<StateAction> actions = accountWorkflow.GetActions();

            LoginAction login = (LoginAction)actions.First();

            login.Login(new LoginCredentials("username", ""));

            var actionsPostLogin = accountWorkflow.GetActions();

            var postLoginAction = actionsPostLogin.First();

            Assert.That(postLoginAction, Is.TypeOf<LoginAction>());
        }

        [Test]
        public void OnceLoggedInCanLogout()
        {
            var accountWorkflow = new AccountWorkflow();
            IEnumerable<StateAction> actions = accountWorkflow.GetActions();

            LoginAction login = (LoginAction)actions.First();

            login.Login(new LoginCredentials("username", "password"));

            var actionsPostLogin = accountWorkflow.GetActions();

            var postLoginAction = (LogoutAction)actionsPostLogin.First();

            postLoginAction.Logout();

            actions = accountWorkflow.GetActions();
            login = (LoginAction)actions.First();
            Assert.That(login, Is.TypeOf<LoginAction>());
        }
    }

    public class SessionId
    {
    }

    public class NullSession : SessionId{ }

    public class LoginCredentials
    {
        public LoginCredentials(string username, string password)
        {
            Password = password;
        }

        public string Password { get; private set; }
    }

    public class StateAction
    {
    }

    public class AccountWorkflow
    {
        private bool _isAuthenticated;
        public IEnumerable<StateAction> GetActions()
        {
            if (_isAuthenticated)
            {
                var loggedIn = new LoggedInState(HasLoggedOut);
                return loggedIn.GetActions();
            }
            var state = new LoggedOutState(HasLoggedIn);
            return state.GetActions();
        }

        private void HasLoggedIn()
        {
            _isAuthenticated = true;
        }

        private void HasLoggedOut()
        {
            _isAuthenticated = false;
        }
    }

    public class LoggedInState
    {
        private readonly Action _hasLoggedOut;

        public LoggedInState(Action hasLoggedOut)
        {
            _hasLoggedOut = hasLoggedOut;
        }

        public IEnumerable<LogoutAction> GetActions()
        {
            return new List<LogoutAction>() { new LogoutAction(_hasLoggedOut) };
        }
    }

    public class LogoutAction : StateAction
    {
        private readonly Action _hasLoggedOut;

        public LogoutAction(Action hasLoggedOut)
        {
            _hasLoggedOut = hasLoggedOut;
        }

        public void Logout()
        {
            _hasLoggedOut.Invoke();
        }
    }

    public class LoggedOutState
    {
        private readonly Action _hasLoggedIn;

        public LoggedOutState(Action hasLoggedIn)
        {
            _hasLoggedIn = hasLoggedIn;
        }

        public IEnumerable<LoginAction> GetActions()
        {
            return new List<LoginAction>(){new LoginAction(_hasLoggedIn)};
        }
    }

    public class LoginAction : StateAction
    {
        private readonly Action _hasLoggedIn;

        public LoginAction(Action hasLoggedIn)
        {
            _hasLoggedIn = hasLoggedIn;
        }

        public SessionId Login(LoginCredentials loginCredentials)
        {
            if (loginCredentials.Password == string.Empty)
                return new NullSession();
            _hasLoggedIn.Invoke();
            return new SessionId();
        }
    } 
}
