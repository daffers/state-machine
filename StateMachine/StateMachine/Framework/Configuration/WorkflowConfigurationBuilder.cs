using System.Collections.Generic;

namespace StateMachine.Framework.Configuration
{
    public class WorkflowConfigurationBuilder
    {
        private readonly List<WorkflowConfiguration> _configurations;

        public WorkflowConfigurationBuilder()
        {
            _configurations = new List<WorkflowConfiguration>();
        }
        
        public WorkflowConfiguration ConfigureWorkflow(string workflowName)
        {
            var configuration = new WorkflowConfiguration(workflowName);
            _configurations.Add(configuration);
            return configuration;
        }

        public IEnumerable<WorkflowConfiguration> BuildConfiguration()
        {
            //TODO validate?
            return _configurations;
        }
    }
}