using System.Collections;
using NUnit.Framework;
using StateMachine.Framework;

namespace StateMachinesTests
{
    [TestFixture]
    public class TestToDriveWorkflowLocator
    {
        [Test]
        public void CanCreateWorkflowFacade()
        {
            var classUnderTest = new WorkflowLocator();
        }

        [Test]
        public void CanFindAllAvailableInitialActions()
        {
            var classUnderTest = new WorkflowLocator();
            /*
             * Questions: 
             * When we get an action, does it contain version information - No we are just returning type
             * Do we therefore need a wrapper for it
             * 
             * The the action responses do need a wrapper that will carry any workflowinstanceid as well as information about invalid params etc
             * 
             * So how does the workflow expose the available versions and what about the idea of that being a context....
             * Possibly the 1.0/uk/cl can simply be a single descriminator?
             */

        }
    }

    public class WorkflowLocator
    {
    }
}