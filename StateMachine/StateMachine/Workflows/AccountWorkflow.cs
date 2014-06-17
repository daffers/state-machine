using System.Collections.Generic;
using StateMachine.Framework;
using StateMachine.TransitionRules;
using StateMachine.WorkflowStates;

namespace StateMachine.Workflows
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