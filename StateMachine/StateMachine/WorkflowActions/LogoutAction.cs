using StateMachine.Framework;
using StateMachine.WorkflowEvents;

namespace StateMachine.WorkflowActions
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

    }
}