using System.Collections.Generic;
using System.Linq;

namespace StateMachine.Framework.Configuration
{
    public class WorkflowDiscoverer
    {
        private readonly IEnumerable<WorkflowConfiguration> _buildConfiguration;

        public WorkflowDiscoverer(IEnumerable<WorkflowConfiguration> buildConfiguration)
        {
            _buildConfiguration = buildConfiguration;
        }

        public IEnumerable<WorkflowDescriptor> GetConfiguredWorkflows()
        {
            return (
                from workflowConfiguration in _buildConfiguration 
                from version in workflowConfiguration.WorkflowVersions 
                select new WorkflowDescriptor(workflowConfiguration.Name, version.Key)
                ).ToList();
        }
    }
}