using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PubSub.Publishers;
using PubSub.Subscribers;
using PubSub.Messages;

namespace PubSub.Brokers
{
    abstract public class MessageBroker
    {
        public MessageBroker()
        {
            Publishers = new List<Publisher>();
            SubscribersPerTopic = new Dictionary<Topic, List<Subscriber>>();
        }

        public List<Publisher> Publishers { get; set; }
        public Dictionary<Topic,List<Subscriber>> SubscribersPerTopic { get; set; }
        public int Id { get; set; }
        abstract public void AddSubscriber(Subscriber subscriber);
        abstract public void PublishNewMessage(Message message);
    }
}
