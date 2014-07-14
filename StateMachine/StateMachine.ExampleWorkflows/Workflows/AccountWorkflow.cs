using System.Collections.Generic;
using StateMachine.ExampleWorkflows.TransitionRules;
using StateMachine.ExampleWorkflows.WorkflowStates;
using StateMachine.Framework;

namespace StateMachine.ExampleWorkflows.Workflows
{
    public class AccountWorkflow : Workflow
    {
        public AccountWorkflow() : base(new MessageWorkflowState())
        {
        }

        public override List<TransitionRule> TransitionRules
        {
            get
            {
                return new List<TransitionRule>()
                {
                    new LoginTransitionRule(),
                    new LogoutTransitionRule()
                };
            }
        }

        public override WorkflowState StartingState
        {
            get { return new LoggedOutState(this); }
        }
    }
}