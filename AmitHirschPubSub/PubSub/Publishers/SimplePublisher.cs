using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PubSub.Brokers;
using PubSub.Messages;

namespace PubSub.Publishers
{
    public class SimplePublisher: Publisher
    {
        public override void Publish(MessageBroker broker, Message message)
        {
            broker.PublishNewMessage(message);
        }
    }
}
