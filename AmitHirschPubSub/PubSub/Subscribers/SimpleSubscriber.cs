using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PubSub.Brokers;
using PubSub.Messages;
using System.Diagnostics;

namespace PubSub.Subscribers
{
    public class SimpleSubscriber : Subscriber
    {
        public override void Register(MessageBroker broker)
        {
            broker.AddSubscriber(this);
        }

        public override void ReadMessage(Message message)
        {
            Trace.WriteLine(String.Format("Subsbcriber {0} Read This Message:' {1} ', Message Id was {2}, Topic Was {3}", Id, message.Text, message.Id, message.Topic));
        }
    }
}
