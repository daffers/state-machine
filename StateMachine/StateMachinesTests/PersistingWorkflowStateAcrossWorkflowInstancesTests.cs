using NUnit.Framework;

namespace StateMachinesTests
{
    [TestFixture]
    public class PersistingWorkflowStateAcrossWorkflowInstancesTests
    {
        [Test]
        public void CanCreateAWorkflowStateStore()
        {
            var store = new WorkflowStateStore();
        }

        [Test]
        public void Can()
        {

        }
    }

    public class WorkflowStateStore
    {
    }
}