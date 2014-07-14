using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using StateMachine;
using StateMachine.Framework;

namespace StateMachinesTests
{
    [TestFixture]
    public class WorkflowDiscoveryTests
    {
        [Test]
        public void CanConfigureASingleWorkflow()
        {
            var workflowConfigurationBuilder = new WorkflowConfigurationBuilder();
            workflowConfigurationBuilder
                .ConfigureWorkflow("LoginWorkflow")
                    .WithVersion(1)
                    .ImplementedBy(typeof (TwoFactorLoginWorkflow));

            var discoverer = new WorkflowDiscoverer(workflowConfigurationBuilder.BuildConfiguration());

            IEnumerable<WorkflowDescriptor> workflowDescriptors = discoverer.GetConfiguredWorkflows();

            Assert.That(workflowDescriptors.Count(), Is.EqualTo(1));

            var firstWorkflowDescriptor = workflowDescriptors.First();
            
            Assert.That(firstWorkflowDescriptor.Name, Is.EqualTo("LoginWorkflow"));
            Assert.That(firstWorkflowDescriptor.Version, Is.EqualTo(1));
        }

        [Test]
        public void TestName()
        {
            var workflowConfigurationBuilder = new WorkflowConfigurationBuilder();
            workflowConfigurationBuilder
                .ConfigureWorkflow("LoginWorkflow")
                    .WithVersion(1)
                    .ImplementedBy(typeof(TwoFactorLoginWorkflow))
                    .WithVersion(2)
                    .WithActionOverride<TwoFactorLoginAction, ThreeFactorLoginAction>()
                    .ImplementedBy(typeof(ThreeFactorLoginWorkflow));
        }
    }

    public class WorkflowConfigurationBuilder
    {
        private readonly List<WorkflowConfiguration> _configurations;

        public WorkflowConfigurationBuilder()
        {
            _configurations = new List<WorkflowConfiguration>();
        }
        
        public WorkflowConfiguration ConfigureWorkflow(string workflowName)
        {
            var configuration = new WorkflowConfiguration();
            _configurations.Add(configuration);
            return configuration;
        }

        public object BuildConfiguration()
        {
            return null;
        }
    }

    public class WorkflowConfiguration
    {
        
        private readonly Dictionary<int, WorkflowVersionConfiguration> _versions;

        public WorkflowConfiguration()
        {
            _versions = new Dictionary<int, WorkflowVersionConfiguration>();
        }

        public WorkflowVersionConfiguration WithVersion(int version)
        {
            var config = new WorkflowVersionConfiguration(this);
            _versions[version] = config;
            return config;
        }
    }

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
        
        public WorkflowVersionConfiguration WithActionOverride<TOriginalAction, TOverideAction>() 
            where TOriginalAction : WorkflowAction
            where TOverideAction : WorkflowAction
        {
            _actionOverrides[typeof(TOriginalAction)] = (typeof (TOverideAction));
            return this;
        }

        public WorkflowConfiguration ImplementedBy(Type type)
        {
            _implementation = type;
            return _workflowConfiguration;
        }
    }

    public class WorkflowDescriptor
    {
        public string Name { get; private set; }
        public int Version { get; private set; }
    }

    public class WorkflowDiscoverer
    {
        public WorkflowDiscoverer(object buildConfiguration)
        {
        }

        public IEnumerable<WorkflowDescriptor> GetConfiguredWorkflows()
        {
            return null;
        }
    }

    public class TwoFactorLoginWorkflow : Workflow
    {
        public TwoFactorLoginWorkflow() : base(null)
        {
        }

        public override List<TransitionRule> TransitionRules
        {
            get { throw new System.NotImplementedException(); }
        }

        public override WorkflowState StartingState
        {
            get { throw new System.NotImplementedException(); }
        }
    }

    public class ThreeFactorLoginWorkflow : Workflow
    {
        public ThreeFactorLoginWorkflow() : base(null)
        {
        }

        public override List<TransitionRule> TransitionRules
        {
            get { throw new System.NotImplementedException(); }
        }

        public override WorkflowState StartingState
        {
            get { throw new System.NotImplementedException(); }
        }
    }

    public class TwoFactorLoginAction : WorkflowAction
    {
    }

    public class ThreeFactorLoginAction : WorkflowAction
    { }
}