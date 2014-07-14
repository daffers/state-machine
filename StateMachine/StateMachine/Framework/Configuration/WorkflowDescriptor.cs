namespace StateMachine.Framework.Configuration
{
    public class WorkflowDescriptor
    {
        public WorkflowDescriptor(string name, int version)
        {
            Name = name;
            Version = version;
        }

        public string Name { get; private set; }
        public int Version { get; private set; }
    }
}