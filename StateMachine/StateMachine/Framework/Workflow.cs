using System;
using System.Collections.Generic;

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

        public IEnumerable<WorkflowAction> GetActions()
        {
            if (_currentState == null)
                _currentState = StartingState;
            return _currentState.GetActions();
        }

        public IEnumerable<WorkflowAction> GetActions2()
        {
            if (_currentState == null)
                _currentState = StartingState;

            var actionTypes = _currentState.GetAvailableActions();

            var wa = new List<WorkflowAction>();
            foreach (var actionType in actionTypes)
            {
                wa.Add(Activator.CreateInstance(actionType,this) as WorkflowAction);
            }

            return wa;
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