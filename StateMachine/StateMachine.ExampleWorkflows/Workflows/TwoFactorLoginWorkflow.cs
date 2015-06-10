using System.Collections.Generic;
using StateMachine.ExampleWorkflows.WorkflowStates;
using StateMachine.Framework;

namespace StateMachine.ExampleWorkflows.Workflows
{
    public class TwoFactorLoginWorkflow : Workflow
    {
        public TwoFactorLoginWorkflow() : base(null)
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