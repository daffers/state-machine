using System.Collections.Generic;

namespace StateMachine.Framework
{
    public abstract class WorkflowState
    {
        public abstract IEnumerable<StateAction> GetActions();
    }

    public abstract class Workflow
    {
        public abstract void HandleEvent(WorkflowEvent workflowEvent);
    }
}