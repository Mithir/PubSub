using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PubSub.Messages;
using PubSub.Brokers;

namespace PubSub.Publishers
{
    public abstract class Publisher
    {
        abstract public void Publish(MessageBroker broker, Message message);
        public int Id { get; set; }
    }
}
