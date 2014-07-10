using System;

namespace StateMachine.Framework
{
    public abstract class WorkflowAction
    {
        public abstract object Execute(object input);
    }

    public abstract class WorkflowAction<TInput, TResult> : WorkflowAction
    {
        public override object Execute(object input)
        {
            if (input.GetType() != typeof(TInput))
                throw new ArgumentException();

            return Execute((TInput) input);
        }

        protected abstract TResult Execute(TInput input);
    }
  
}