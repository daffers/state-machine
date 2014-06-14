using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace StateMachinesTests
{
    [TestFixture]
    public class TwoStatesStateMachineTest
    {
        [Test]
        public void CanCreateLoggedOutState()
        {
            var loggedOut = new LoggedOutState();
        }

        [Test]
        public void CanCreateLoggedInState()
        {
            var loggedIn = new LoggedInState();
        }

        [Test]
        public void TheValidActionForLoggedOutStateIsToLogin()
        {
            var loggedOut = new LoggedOutState();
            IEnumerable<LoginAction> actions = loggedOut.GetActions();

            Assert.That(actions.Count(), Is.EqualTo(1));
            Assert.That(actions.First(), Is.TypeOf<LoginAction>());
        }

        [Test]
        public void TheValidActionForLoggedInStateIsToLogout()
        {
            var loggedin = new LoggedInState();
            IEnumerable<LogoutAction> actions = loggedin.GetActions();

            Assert.That(actions.Count(), Is.EqualTo(1));
            Assert.That(actions.First(), Is.TypeOf<LogoutAction>());
        }

        [Test]
        public void CanCreateAnAccountWorkflow()
        {
            var accountWorkflow = new AccountWorkflow();
        }

        [Test]
        public void CanGetValidActionsForAccountWorkflow()
        {
            var accountWorkflow = new AccountWorkflow();
            IEnumerable<StateAction> actions = accountWorkflow.GetActions();
        }

        [Test]
        public void WhenTheWorkFlowIsInitializedTheFirstActionIsLogin()
        {
            var accountWorkflow = new AccountWorkflow();
            IEnumerable<StateAction> actions = accountWorkflow.GetActions();

            Assert.That(actions.Count(), Is.EqualTo(1));
            Assert.That(actions.First(), Is.TypeOf<LoginAction>());
        }
    }

    public class StateAction
    {
    }

    public class AccountWorkflow
    {
        public IEnumerable<StateAction> GetActions()
        {
            return new List<StateAction>(){new LoginAction()};
        }
    }

    public class LoggedInState
    {
        public IEnumerable<LogoutAction> GetActions()
        {
            return new List<LogoutAction>() { new LogoutAction() };
        }
    }

    public class LogoutAction{}

    public class LoggedOutState
    {
        public IEnumerable<LoginAction> GetActions()
        {
            return new List<LoginAction>(){new LoginAction()};
        }
    }

    public class LoginAction : StateAction
    {
        
    } 
}
