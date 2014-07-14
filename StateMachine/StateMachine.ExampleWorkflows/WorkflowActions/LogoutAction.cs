using StateMachine.ExampleWorkflows.WorkflowEvents;
using StateMachine.Framework;

namespace StateMachine.ExampleWorkflows.WorkflowActions
{
    public class LogoutAction : WorkflowAction
    {
        private readonly Workflow _workflow;

        public LogoutAction(Workflow workflow)
        {
            _workflow = workflow;
        }

        public void Logout()
        {
            _workflow.HandleEvent(new LoggedOut());
        }

        public override object ExcuteAction(object input)
        {
            _workflow.HandleEvent(new LoggedOut());
            return null;
        }
    }
}