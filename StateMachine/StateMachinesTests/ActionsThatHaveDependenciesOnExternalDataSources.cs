using System;
using System.CodeDom;
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
            var workflow = new GetNamesWorkflow(new MessageWorkflowState(),new NamesDataSource(new List<string>()));
            var firstAction = workflow.GetActions().First();

            var namesFromAction = firstAction.Execute(null);
            Assert.That(namesFromAction, Is.AssignableTo<IEnumerable<string>>());
            var namesList = namesFromAction as IEnumerable<string>;
            Assert.That(namesList, Is.Empty);
        }

        [Test]
        public void WhenOneNameConfiguredInListItIsReturnedByAction()
        {
            var dataSource = new NamesDataSource(new List<string>(){"Bob"});

            var workflow = new GetNamesWorkflow(new MessageWorkflowState(), dataSource);
            var firstAction = workflow.GetActions().First();


            var namesFromAction = firstAction.Execute(null);
            Assert.That(namesFromAction, Is.AssignableTo<IEnumerable<string>>());
            var namesList = namesFromAction as IEnumerable<string>;
            Assert.That(namesList, Contains.Item("Bob"));
        }
    }

    public class ActionFactory
    {
        private readonly NamesDataSource _source;

        public ActionFactory(NamesDataSource source)
        {
            _source = source;
        }

        public WorkflowAction BuildUpAction(Type actionType)
        {
            return new GetNamesListAction(_source);
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
        private readonly NamesDataSource _source;

        public GetNamesWorkflow(MessageWorkflowState state, NamesDataSource source) : base(state)
        {
            _source = source;
        }

        public override List<TransitionRule> TransitionRules
        {
            get { return new List<TransitionRule>() {new NoTransition()}; }
        }

        public override WorkflowState StartingState
        {
            get { return new CanListNamesState(_source); }
        }
    }

    public class NoTransition : TransitionRule
    {
        public override WorkflowState Transition(WorkflowEvent workflowEvent, Workflow workflow, MessageWorkflowState state)
        {
            return null;
        }
    }

    public class CanListNamesState : WorkflowState
    {
        private readonly NamesDataSource _dataSource;

        public CanListNamesState(NamesDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public override IEnumerable<WorkflowAction> GetActions()
        {
            return new List<WorkflowAction>() { new GetNamesListAction(_dataSource)};
        }

        public override IEnumerable<Type> GetAvailableActions()
        {
            return new List<Type>() { typeof(GetNamesListAction) };
        }
    }

    public class GetNamesListAction : WorkflowAction
    {
        private readonly NamesDataSource _dataSource;

        public GetNamesListAction(NamesDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public override object Execute(object input)
        {
            return _dataSource.GetNames().ToList();
        }
    }
}

