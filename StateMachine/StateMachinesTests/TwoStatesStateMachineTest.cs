using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using StateMachine;
using StateMachine.Framework;

namespace StateMachinesTests
{
    [TestFixture]
    public class TwoStatesStateMachineTest
    {
        [Test]
        public void TheValidActionForLoggedOutStateIsToLogin()
        {
            var loggedOut = new LoggedOutState(null, null, null);
            var actions = loggedOut.GetActions();

            Assert.That(actions.Count(), Is.EqualTo(1));
            Assert.That(actions.First(), Is.TypeOf<LoginAction>());
        }

        [Test]
        public void TheValidActionForLoggedInStateIsToLogout()
        {
            var loggedin = new UserLoggedInState(null, null);
            var actions = loggedin.GetActions();

            Assert.That(actions.First(), Is.TypeOf<LogoutAction>());
        }

        [Test]
        public void WhenTheWorkFlowIsInitializedTheFirstActionIsLogin()
        {
            var accountWorkflow = new AccountWorkflow();
            IEnumerable<StateAction> actions = accountWorkflow.GetActions();

            Assert.That(actions.Any(IsLoginAction()), Is.True);
        }

        [Test]
        public void WhenCorrectLoginCredentialsSuppliedResultIsASessionId()
        {
            var loginCredentials = new LoginCredentials("username", "password");
            var loginAction = new LoginAction(new Action(() => {}), null, null);
            var result = loginAction.Login(loginCredentials);

            Assert.That(result, Is.TypeOf<SessionId>());
        }

        [Test]
        public void WhenIncorrectLoginDetailsSuppliedResultIsANullSession()
        {
            var loginCredentials = new LoginCredentials("username", "");
            var loginAction = new LoginAction(null, null, null);
            var result = loginAction.Login(loginCredentials);

            Assert.That(result, Is.TypeOf<NullSession>());
        }

        [Test]
        public void AfterSuccessfulLoginThroughWorflowActionBecomesLogout()
        {
            var accountWorkflow = new AccountWorkflow();
            var actions = accountWorkflow.GetActions();

            var login = (LoginAction)actions.First();

            login.Login(new LoginCredentials("username", "password"));

            var actionsPostLogin = accountWorkflow.GetActions();

            var postLoginAction = actionsPostLogin.First(IsLogoutAction());

            Assert.That(postLoginAction, Is.TypeOf<LogoutAction>());
        }

        [Test]
        public void WhenLoginUnsuccessfulWorkflowActionRemainsAsLogin()
        {
            var accountWorkflow = new AccountWorkflow();
            var actions = accountWorkflow.GetActions();

            var login = (LoginAction)actions.First();

            login.Login(new LoginCredentials("username", ""));

            var actionsPostLogin = accountWorkflow.GetActions();

            var postLoginAction = actionsPostLogin.First();

            Assert.That(postLoginAction, Is.TypeOf<LoginAction>());
        }

        [Test]
        public void OnceLoggedInCanLogout()
        {
            var accountWorkflow = new AccountWorkflow();
            var actions = accountWorkflow.GetActions();

            var login = (LoginAction)actions.First();

            login.Login(new LoginCredentials("username", "password"));

            var actionsPostLogin = accountWorkflow.GetActions();

            var postLoginAction = (LogoutAction)actionsPostLogin.First();

            postLoginAction.Logout();

            actions = accountWorkflow.GetActions();
            login = (LoginAction)actions.First(IsLoginAction());
            Assert.That(login, Is.TypeOf<LoginAction>());
        }

        private static Func<StateAction, bool> IsLogoutAction()
        {
            return action => action.GetType() == typeof(LogoutAction);
        }

        private static Func<StateAction, bool> IsLoginAction()
        {
            return action => action.GetType() == typeof(LoginAction);
        }
    }
}
