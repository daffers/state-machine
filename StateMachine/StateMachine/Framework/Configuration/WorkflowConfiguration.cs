using System.Collections.Generic;

namespace StateMachine.Framework.Configuration
{
    public class WorkflowConfiguration
    {
        private readonly string _workflowName;
        private readonly Dictionary<int, WorkflowVersionConfiguration> _versions;

        public WorkflowConfiguration(string workflowName)
        {
            _workflowName = workflowName;
            _versions = new Dictionary<int, WorkflowVersionConfiguration>();
        }

        public WorkflowVersionConfiguration WithVersion(int version)
        {
            var config = new WorkflowVersionConfiguration(this);
            _versions[version] = config;
            return config;
        }

        internal IDictionary<int, WorkflowVersionConfiguration> WorkflowVersions { get { return _versions; } }
        internal string Name { get { return _workflowName; } }
    }
}