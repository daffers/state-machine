using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using StateMachine.ExampleWorkflows.WorkflowActions;
using StateMachine.Framework.Configuration;

namespace StateMachinesTests.ConfigurationTests
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
    public class StartingWorkflowTests
    {
        [Test]
        public void WhenAWorkflowIsConfiguredItStartingActionsAreAvailable()
        {
            var workflowConfigurationBuilder = new WorkflowConfigurationBuilder();
            workflowConfigurationBuilder
                .ConfigureWorkflow("LoginWorkflow")
                .WithVersion(1)
                .ImplementedBy(typeof(TwoFactorLoginWorkflow));

            var actionLoader = new WorkflowActionLoader();

            var descriptor = new WorkflowDescriptor("LoginWorkflow", 1);

            IEnumerable<Type> startingActions = actionLoader.GetWorkflowStartingActions(descriptor);

            Assert.That(startingActions.Count(), Is.EqualTo(1));
            var firstActionType = startingActions.First();

            Assert.That(firstActionType, Is.EqualTo(typeof(LoginAction)));
        }
    }

    public class WorkflowActionLoader
    {
        public IEnumerable<Type> GetWorkflowStartingActions(WorkflowDescriptor descriptor)
        {
            return new List<Type>();
        }
    }

    //TODO: tests around versioning of the state object

    //should design a few different workflows...
    /*
     *  1. We have A login and edit message test
     *  2. We should have a 3 factor login to edit message variation
     *  3. We need a workflow with two version - different by the state object
     */
}