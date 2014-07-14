using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using StateMachine;
using StateMachine.ExampleWorkflows;
using StateMachine.Framework;

namespace StateMachinesTests
{
    [TestFixture]
    public class TestToDriveVersionedWorkflows
    {
        [Test]
        public void FirstActionOnVersion1StatesVersion()
        {
            var versionWorkflow = new VersionTestWorkflow(new MessageWorkflowState());
            var firstAction = (GetVersionAction)versionWorkflow.GetActions().First();

            Assert.That(firstAction.GetVersionText(), Is.EqualTo("version 1.0"));
        }

        [Test]
        public void FirstActionOnVersion2StateVersion()
        {
            var versionWorkflow = new VersionTestWorkflow2(new MessageWorkflowState());

            var firstAction = (GetVersionAction2)versionWorkflow.GetActions().First();

            Assert.That(firstAction.GetVersionText(), Is.EqualTo("version 2.0"));
        }

        [Test]
        public void ShouldBeAbleToSpecifyWorkflowVersion1WithOutKnowingClassName()
        {
            var workflowFactory = new WorkflowFactory();
            var versionWorkflow = workflowFactory.BuildWorkflow<VersionTestWorkflow>(1);

            var firstAction = versionWorkflow.GetActions().First();

            Assert.That(firstAction.ExcuteAction(null), Is.EqualTo("version 1.0"));
        }

        [Test]
        public void ShouldBeAbleToSpecifyWorkflowVersion2WithOutKnowingClassName()
        {
            var workflowFactory = new WorkflowFactory();
            var versionWorkflow = workflowFactory.BuildWorkflow<VersionTestWorkflow>(2);

            var firstAction = versionWorkflow.GetActions().First();

            Assert.That(firstAction.ExcuteAction(null), Is.EqualTo("version 2.0"));
        }
    }

    public class WorkflowFactory
    {
        public T BuildWorkflow<T>(int versionNumber) where T : Workflow
        {
            if (versionNumber == 2)
                return (T)(Workflow)(new VersionTestWorkflow2(new MessageWorkflowState()));
            return (T)(Workflow)(new VersionTestWorkflow(new MessageWorkflowState()));
        }
    }

    public class VersionTestWorkflow2 : VersionTestWorkflow
    {
        public VersionTestWorkflow2(MessageWorkflowState state) : base(state)
        {
        }

        public override WorkflowState StartingState
        {
            get { return new CanGetVersionTextState2(); }
        }
    }

    public class CanGetVersionTextState2 : CanGetVersionTextState
    {
        public override IEnumerable<WorkflowAction> GetActions()
        {
            return new List<WorkflowAction>() {new GetVersionAction2()};
        }
    }

    public class GetVersionAction2 : GetVersionAction
    {
        public override object ExcuteAction(object input)
        {
            return "version 2.0";
        }

        public new string GetVersionText()
        {
            return "version 2.0";
        }
    }

    public class VersionTestWorkflow : Workflow
    {
        public VersionTestWorkflow(MessageWorkflowState state) : base(state)
        {
        }

        public override List<TransitionRule> TransitionRules
        {
            get { return new List<TransitionRule>() {new NoTransition()}; }
        }

        public override WorkflowState StartingState
        {
            get { return new CanGetVersionTextState(); }
        }
    }

    public class CanGetVersionTextState : WorkflowState
    {
        public override IEnumerable<WorkflowAction> GetActions()
        {
            return new List<WorkflowAction>() {new GetVersionAction()};
        }
    }

    public class GetVersionAction : WorkflowAction
    {
        public override object ExcuteAction(object input)
        {
            return "version 1.0";
        }

        public string GetVersionText()
        {
            return "version 1.0";
        }
    }

    public class NoTransition : TransitionRule
    {
        public override WorkflowState Transition(IWorkflowEvent workflowEvent, Workflow workflow, object state)
        {
            return null;
        }
    }
}