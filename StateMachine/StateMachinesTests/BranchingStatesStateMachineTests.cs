using System;
using System.Linq;
using NUnit.Framework;
using StateMachine;
using StateMachine.ExampleWorkflows;
using StateMachine.ExampleWorkflows.WorkflowActions;
using StateMachine.ExampleWorkflows.Workflows;
using StateMachine.Framework;

namespace StateMachinesTests
{
    [TestFixture]
    public class BranchingStatesStateMachineTests
    {
        [Test]
        public void TheFirstActionForMessageWorkflowIsLogin()
        {
            var workflow = new MessageWorkflow();
            var actions = workflow.GetActions();

            var firstAction = actions.First();

            Assert.That(firstAction, Is.TypeOf<LoginAction>());
        }

        [Test]
        public void CanLoginToTheMesageWorkflowAsUser()
        {
            var workflow = new MessageWorkflow();
            LoginUser(workflow);

            var loginActionsAvailable = workflow.GetActions().Any(IsLoginAction());
            Assert.That(loginActionsAvailable, Is.False);
        }

        [Test]
        public void CanLoginToTheMessageWorkflowAsAnAdmin()
        {
            var workflow = new MessageWorkflow();
            LoginAdministrator(workflow);

            var loginActionsAvailable = workflow.GetActions().Any(IsLoginAction());
            Assert.That(loginActionsAvailable, Is.False);
        }

        [Test]
        public void WhenLoggedInAsAUserCanOnlyLogoutAndViewMessage()
        {
            var workflow = new MessageWorkflow();
            LoginUser(workflow);

            var loginActionsAvailable = workflow.GetActions().Any(IsLogoutAction());
            Assert.That(loginActionsAvailable, Is.True);
            var viewMessageAvailable = workflow.GetActions().Any(IsViewMessageAction());
            Assert.That(viewMessageAvailable, Is.True);
        }

        [Test]
        public void CanViewTheMessageOnceLoggedIn()
        {
            var workflow = new MessageWorkflow();
            LoginUser(workflow);

            var viewMessage = (ViewMessageAction)workflow.GetActions().First(IsViewMessageAction());

            var message = viewMessage.GetMessage();

            Assert.That(message, Is.EqualTo(string.Empty));
        }

        [Test]
        public void LoggedInAdministratorCanEditTheMessage()
        {
            var workflow = new MessageWorkflow();
            LoginAdministrator(workflow);

            var editMessageAvalable = workflow.GetActions().Any(IsEditMessageAction());
            Assert.That(editMessageAvalable, Is.True);
        }

        [Test]
        public void WhenAdminEditsMessageCanViewMessage()
        {
            var workflow = new MessageWorkflow();
            LoginAdministrator(workflow);

            var editMessageAction = (EditMessageAction)workflow.GetActions().First(IsEditMessageAction());

            const string helloWorld = "Hello World";
            editMessageAction.SetMessage(helloWorld);

            var viewMessageAction = (ViewMessageAction)workflow.GetActions().First(IsViewMessageAction());
            var messageRetrieved = viewMessageAction.GetMessage();

            Assert.That(messageRetrieved, Is.EqualTo(helloWorld));
        }

        [Test]
        public void AdminnCanLoginEditMessageLogoutAndUserCanLoginAndReadMessage()
        {
            var workflow = new MessageWorkflow();
            LoginAdministrator(workflow);

            var editMessageAction = (EditMessageAction)workflow.GetActions().First(IsEditMessageAction());
    
            const string helloWorld = "Hello World";
            editMessageAction.SetMessage(helloWorld);

            var logoutAction = (LogoutAction) workflow.GetActions().First(IsLogoutAction());
            logoutAction.Logout();

            LoginUser(workflow);
            var viewMessageAction = (ViewMessageAction)workflow.GetActions().First(IsViewMessageAction());
            var messageRetrieved = viewMessageAction.GetMessage();

            Assert.That(messageRetrieved, Is.EqualTo(helloWorld));
        }

        private static void LoginAdministrator(MessageWorkflow workflow)
        {
            var actions = workflow.GetActions();

            var firstAction = (LoginAction) actions.First();

            firstAction.Login(new LoginCredentials("Administrator", "Password"));
        }

        private static void LoginUser(MessageWorkflow workflow)
        {
            var actions = workflow.GetActions();

            var firstAction = (LoginAction) actions.First(IsLoginAction());

            firstAction.Login(new LoginCredentials("User", "Password"));
        }

        private static Func<WorkflowAction, bool> IsLoginAction()
        {
            return action => action.GetType() == typeof(LoginAction);
        }

        private static Func<WorkflowAction, bool> IsEditMessageAction()
        {
            return action => action.GetType() == typeof(EditMessageAction);
        }

        private static Func<WorkflowAction, bool> IsViewMessageAction()
        {
            return action => action.GetType() == typeof(ViewMessageAction);
        }

        private static Func<WorkflowAction, bool> IsLogoutAction()
        {
            return action => action.GetType() == typeof(LogoutAction);
        }
    }
}