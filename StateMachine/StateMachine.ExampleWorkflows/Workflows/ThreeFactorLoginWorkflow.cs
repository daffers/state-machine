using System.Collections.Generic;
using StateMachine.ExampleWorkflows.WorkflowStates;
using StateMachine.Framework;

namespace StateMachine.ExampleWorkflows.Workflows
{
    public class ThreeFactorLoginWorkflow : Workflow
    {
        public ThreeFactorLoginWorkflow() : base(null)
        {
        }

        public override List<TransitionRule> TransitionRules
        {
            get { throw new System.NotImplementedException(); }
        }

        public override WorkflowState StartingState
        {
            get { return new LoggedOutState(this); }
        }
    }
}