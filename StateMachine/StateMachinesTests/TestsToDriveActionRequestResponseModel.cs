using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using StateMachine;
using StateMachine.ExampleWorkflows;
using StateMachine.Framework;

namespace StateMachinesTests
{
    [TestFixture]
    public class TestsToDriveActionRequestResponseModel
    {
        [Test]
        public void TheFirstActionIsAnActionToAddTwoNumbers()
        {
            var workflow = new AddTwoNumbersWorkflow(new MessageWorkflowState());
            var firstAction = workflow.GetActions().First();

            Assert.That(firstAction, Is.TypeOf<AddTwoNumbersAction>());
        }

        [Test]
        public void CanAddTwoNumbersTogether()
        {
            var workflow = new AddTwoNumbersWorkflow(new MessageWorkflowState());
            var firstAction = workflow.GetActions().First() as AddTwoNumbersAction;

            var result = firstAction.Add(new AddTwoNumbersRequest(1, 1));

            Assert.That(result.Result, Is.EqualTo(2));
        }
    }

    [TestFixture]
    public class TestsToDriveSinglePointOfEntryForRunningWorkflows
    {
        [Test]
        public void TestMethodName()
        {

        }
    }

    public class AddTwoNumbersAction : WorkflowAction
    {
        public AddTwoNumbersResponse Add(AddTwoNumbersRequest addTwoNumbersRequest)
        {
            return new AddTwoNumbersResponse(addTwoNumbersRequest.X + addTwoNumbersRequest.Y);
        }
    }

    public class AddTwoNumbersRequest
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public AddTwoNumbersRequest(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class AddTwoNumbersResponse
    {
        public int Result { get; private set; }

        public AddTwoNumbersResponse(int result)
        {
            Result = result;
        }
    }

    public class AddTwoNumbersWorkflow : Workflow
    {
        public AddTwoNumbersWorkflow(MessageWorkflowState state) : base(state)
        {
        }

        public override List<TransitionRule> TransitionRules
        {
            get { throw new System.NotImplementedException(); }
        }

        public override WorkflowState StartingState
        {
            get { return new AddNumbersState(); }
        }
    }

    public class AddNumbersState : WorkflowState
    {
        public override IEnumerable<WorkflowAction> GetActions()
        {
            return new List<WorkflowAction>() {new AddTwoNumbersAction()};
        }
    }
}