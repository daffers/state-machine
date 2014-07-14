using System.Collections.Generic;

namespace StateMachine.Framework
{
    public abstract class Workflow
    {
        protected Workflow(object state)
        {
            _state = state;
        }

        private WorkflowState _currentState;
        private readonly object _state;
        
        public abstract List<TransitionRule> TransitionRules { get; }
        public abstract WorkflowState StartingState { get; }

        public IEnumerable<WorkflowAction> GetActions()
        {
            if (_currentState == null)
                _currentState = StartingState;
            return _currentState.GetActions();
        }

        public void HandleEvent(IWorkflowEvent workflowEvent)
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