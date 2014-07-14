using System;
using System.Collections.Generic;

namespace StateMachine.Framework.Configuration
{
    public class WorkflowVersionConfiguration
    {
        private readonly WorkflowConfiguration _workflowConfiguration;
        private readonly Dictionary<Type, Type> _actionOverrides;
        private Type _implementation;

        public WorkflowVersionConfiguration(WorkflowConfiguration workflowConfiguration)
        {
            _actionOverrides = new Dictionary<Type, Type>();
            _workflowConfiguration = workflowConfiguration;
        }
        
        public WorkflowVersionConfiguration WithActionOverride<TOriginalAction, TOverrideAction>() 
            where TOriginalAction : WorkflowAction
            where TOverrideAction : WorkflowAction
        {
            _actionOverrides[typeof(TOriginalAction)] = (typeof (TOverrideAction));
            return this;
        }

        public WorkflowConfiguration ImplementedBy(Type type)
        {
            _implementation = type;
            return _workflowConfiguration;
        }
    }
}