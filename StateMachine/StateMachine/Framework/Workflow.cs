using System.Collections.Generic;
using StateMachine.Workflows;

namespace StateMachine.Framework
{
    public abstract class Workflow
    {
        protected Workflow(MessageWorkflowState state)
        {
            _state = state;
        }

        private WorkflowState _currentState;
        private readonly MessageWorkflowState _state;
        
        public abstract List<TransitionRule> TransitionRules { get; }
        public abstract WorkflowState StartingState { get; }

        public IEnumerable<StateAction> GetActions()
        {
            if (_currentState == null)
                _currentState = StartingState;
            return _currentState.GetActions();
        }

        protected internal void HandleEvent(WorkflowEvent workflowEvent)
        {
            foreach (var rule in TransitionRules)
            {
                var nextState = rule.Transition(workflowEvent, this, _state);
                if (nextState != null)
                    _currentState = nextState;
            }
        }
    }
}