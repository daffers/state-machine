using System.Collections.Generic;
using StateMachine.ExampleWorkflows.TransitionRules;
using StateMachine.ExampleWorkflows.WorkflowStates;
using StateMachine.Framework;

namespace StateMachine.ExampleWorkflows.Workflows
{
    public class MessageWorkflow : Workflow
    {
        public MessageWorkflow() : base(new MessageWorkflowState() { CurrentMessage = string.Empty })
        {
        }

        public override List<TransitionRule> TransitionRules
        {
            get
            {
                return new List<TransitionRule>()
                {
                    new LoginTransitionRule(),
                    new LogoutTransitionRule(),
                    new AdminLoginTransitionRule()
                };
            }
        }

        public override WorkflowState StartingState
        {
            get { return new LoggedOutState(this); }
        }
    }
}