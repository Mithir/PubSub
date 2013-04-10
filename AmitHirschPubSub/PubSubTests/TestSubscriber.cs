using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PubSub.Subscribers;

namespace PubSubTests
{
    class TestSubscriber : Subscriber
    {
        public Boolean MessageWasRead { get; set; }
        public override void Register(PubSub.Brokers.MessageBroker broker)
        {
            broker.AddSubscriber(this);
        }

        public override void ReadMessage(PubSub.Messages.Message message)
        {
            MessageWasRead = true;
        }
    }
}
