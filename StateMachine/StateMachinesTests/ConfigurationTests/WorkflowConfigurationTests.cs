using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using StateMachine.Framework.Configuration;

namespace StateMachinesTests.ConfigurationTests
{
    [TestFixture]
    public class WorkflowConfigurationTests
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
        public void CanConfigureAWorkflowWithTwoVersions()
        {
            var workflowConfigurationBuilder = new WorkflowConfigurationBuilder();
            workflowConfigurationBuilder
                .ConfigureWorkflow("LoginWorkflow")
                .WithVersion(1)
                .ImplementedBy(typeof(TwoFactorLoginWorkflow))
                .WithVersion(2)
                .ImplementedBy(typeof(ThreeFactorLoginWorkflow));

            var discoverer = new WorkflowDiscoverer(workflowConfigurationBuilder.BuildConfiguration());

            IEnumerable<WorkflowDescriptor> workflowDescriptors = discoverer.GetConfiguredWorkflows();

            Assert.That(workflowDescriptors.Count(), Is.EqualTo(2));

            var firstWorkflowDescriptor = workflowDescriptors.First();

            Assert.That(firstWorkflowDescriptor.Name, Is.EqualTo("LoginWorkflow"));
            Assert.That(firstWorkflowDescriptor.Version, Is.EqualTo(1));

            var lastWorkflowDescriptor = workflowDescriptors.Last();

            Assert.That(lastWorkflowDescriptor.Name, Is.EqualTo("LoginWorkflow"));
            Assert.That(lastWorkflowDescriptor.Version, Is.EqualTo(2));
        }
    }

    [TestFixture]
    public class StartingWorflowTests
    {
        [Test]
        public void TestName()
        {

        }
    }
}