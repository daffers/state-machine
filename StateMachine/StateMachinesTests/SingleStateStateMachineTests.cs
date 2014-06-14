using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace StateMachinesTests.SingleState
{
    [TestFixture]
    public class SingleStateStateMachineTests
    {
        [Test]
        public void CanDefineTheSingleState()
        {
            var state = new CanSubmitMessage();
        }

        [Test]
        public void CanDefineASingleAction()
        {
            var sendMessage = new SendMessageAction();
        }

        [Test]
        public void TheOnlyActionAvailableFromFirstStateIsSendMessage()
        {
            var state = new CanSubmitMessage();
            var actions = state.GetActions();
            
            Assert.That(actions.Count(), Is.EqualTo(1));
            Assert.That(actions.First(), Is.TypeOf<SendMessageAction>());
        }

        [Test]
        public void CanInvokeTheSendMessageActionWithAMessage()
        {
            var message = new Message();
            var action = new SendMessageAction();
            action.Execute(message);
        }
    }

    public class Message
    {
    }

    public class SendMessageAction
    {
        public void Execute(Message message)
        {
        }
    }

    public class CanSubmitMessage
    {
        public IEnumerable<SendMessageAction> GetActions()
        {
            return new List<SendMessageAction>() {new SendMessageAction()};
        }
    }
}
