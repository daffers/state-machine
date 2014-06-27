using System;
using System.Collections.Generic;

namespace StateMachine.Framework
{
    public abstract class WorkflowState
    {
        public abstract IEnumerable<WorkflowAction> GetActions();

        public virtual IEnumerable<Type> GetAvailableActions()
        {
            return new List<Type>();
        }
    }
}