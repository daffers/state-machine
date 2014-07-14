using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using StateMachine;
using StateMachine.ExampleWorkflows;
using StateMachine.ExampleWorkflows.WorkflowActions;
using StateMachine.ExampleWorkflows.Workflows;
using StateMachine.Framework;

namespace StateMachinesTests
{
    public class WorkflowFacade
    {
        private readonly AccountWorkflow _accountWorkflow;

        public WorkflowFacade()
        {
            _accountWorkflow = new AccountWorkflow();
        }

        public IEnumerable<Type> GetAvailableActions()
        {
            return _accountWorkflow.GetActions().Select(a => a.GetType());
        }

        public object ExecuteAction(Type actionType, object request)
        {
            var action = _accountWorkflow.GetActions().Single(a => a.GetType() == actionType);
            return action.ExcuteAction(request);
        }
    }

    [TestFixture]
    public class TwoStatesStateMachineTest
    {
        [Test]
        public void WhenTheWorkFlowIsInitializedTheFirstActionIsLogin()
        {
            var facade = new WorkflowFacade();
            var actions = facade.GetAvailableActions();
            
            Assert.That(actions.Any(IsLoginAction()), Is.True);
        }

        [Test]
        public void WhenCorrectLoginCredentialsSuppliedResultIsASessionId()
        {
            var facade = new WorkflowFacade();

            var result = facade.ExecuteAction(typeof(LoginAction), new LoginCredentials("username", "password"));

            Assert.That(result, Is.TypeOf<SessionId>());
        }

        [Test]
        public void WhenIncorrectLoginDetailsSuppliedResultIsANullSession()
        {
            var facade = new WorkflowFacade();

            var result = facade.ExecuteAction(typeof(LoginAction), new LoginCredentials("username", ""));

            Assert.That(result, Is.TypeOf<NullSession>());
        }

        [Test]
        public void AfterSuccessfulLoginThroughWorkflowActionBecomesLogout()
        {
            var facade = new WorkflowFacade();
            var actions = facade.GetAvailableActions();
            
            facade.ExecuteAction(actions.First(), new LoginCredentials("username", "password"));

            var actionsPostLogin = facade.GetAvailableActions();

            var postLoginAction = actionsPostLogin.First(IsLogoutAction());

            Assert.That(postLoginAction, Is.EqualTo(typeof(LogoutAction)));
        }

        [Test]
        public void WhenLoginUnsuccessfulWorkflowActionRemainsAsLogin()
        {
            var facade = new WorkflowFacade();
            
            facade.ExecuteAction(typeof(LoginAction), new LoginCredentials("username", ""));

            var actionsPostLogin = facade.GetAvailableActions();

            var postLoginAction = actionsPostLogin.First();

            Assert.That(postLoginAction, Is.EqualTo(typeof(LoginAction)));
        }

        [Test]
        public void OnceLoggedInCanLogout()
        {
            var facade = new WorkflowFacade();

            facade.ExecuteAction(typeof(LoginAction), new LoginCredentials("username", "password"));

            facade.ExecuteAction(typeof (LogoutAction), null);

            var actions = facade.GetAvailableActions();
            var loginAvailable = actions.Any(IsLoginAction());
            Assert.That(loginAvailable, Is.True);
        }

        private static Func<Type, bool> IsLogoutAction()
        {
            return action => action == typeof(LogoutAction);
        }

        private static Func<Type, bool> IsLoginAction()
        {
            return action => action == typeof(LoginAction);
        }
    }
}
