using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PubSub.Messages;
using PubSub.Brokers;

namespace PubSub.Subscribers
{
    public abstract class Subscriber
    {
        public List<Topic> Topics { get; set; }

        public int Id { get; set; }

        abstract public void Register(MessageBroker broker);

        abstract public void ReadMessage(Message message);
    }
}
