using System;

namespace StateMachine.Framework
{
    public class WorkflowAction
    {
        public virtual object ExcuteAction(object input)
        {
            return null;
        }
    }

    public abstract class WorkflowActionTyped : WorkflowAction
    {
        public abstract object Execute(object input);
    }

    public abstract class WorkflowActionTyped<TInput, TResult> : WorkflowActionTyped
    {
        public override object Execute(object input)
        {
            if (input.GetType() != typeof(TInput))
                throw new ArgumentException();

            return Execute((TInput) input);
        }

        protected abstract TResult Execute(TInput input);
    }

    public class Example : WorkflowActionTyped<int, string>
    {
        protected override string Execute(int input)
        {
            return input.ToString();
        }
    }
}