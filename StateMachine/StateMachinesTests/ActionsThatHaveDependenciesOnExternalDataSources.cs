using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using StateMachine;
using StateMachine.Framework;

namespace StateMachinesTests
{
    [TestFixture]
    public class ActionsThatHaveDependenciesOnExternalDataSources
    {
        [Test]
        public void WhenNoNamesConfiguredWorkflowReturnsEmptyList()
        {
            var workflow = new GetNamesWorkflow(new MessageWorkflowState());
            var firstAction = workflow.GetActions().First();

            var namesFromAction = firstAction.ExcuteAction(null);
            Assert.That(namesFromAction, Is.AssignableTo<IEnumerable<string>>());
            var namesList = namesFromAction as IEnumerable<string>;
            Assert.That(namesList, Is.Empty);
        }

        [Test]
        public void WhenOnNameConfiguredInListItIsReturnedByAction()
        {
            var dataSource = new NamesDataSource(new List<string>(){"Bob"});

            var workflow = new GetNamesWorkflow(new MessageWorkflowState());
            var firstAction = workflow.GetActions().First();

            var namesFromAction = firstAction.ExcuteAction(null);
            Assert.That(namesFromAction, Is.AssignableTo<IEnumerable<string>>());
            var namesList = namesFromAction as IEnumerable<string>;
            Assert.That(namesList, Contains.Item("Bob"));
        }
    }

    public class NamesDataSource
    {
        private readonly List<string> _namesList;

        public NamesDataSource(List<string> namesList)
        {
            _namesList = namesList;
        }

        public IEnumerable<string> GetNames()
        {
            return _namesList;
        }
    }

    public class GetNamesWorkflow : Workflow
    {
        public GetNamesWorkflow(MessageWorkflowState state) : base(state)
        {
        }

        public override List<TransitionRule> TransitionRules
        {
            get { return new List<TransitionRule>() {new NoTransition()}; }
        }

        public override WorkflowState StartingState
        {
            get { return new CanListNamesState(); }
        }
    }

    public class CanListNamesState : WorkflowState
    {
        public override IEnumerable<WorkflowAction> GetActions()
        {
            return new List<WorkflowAction>() { new GetNamesListAction()};
        }
    }

    public class GetNamesListAction : WorkflowAction
    {
        public override object ExcuteAction(object input)
        {
            return new List<string>();
        }
    }
}

