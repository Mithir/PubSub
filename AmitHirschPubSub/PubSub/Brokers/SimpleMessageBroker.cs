using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PubSub.Subscribers;
using PubSub.Publishers;
using PubSub.Messages;
using System.Diagnostics;

namespace PubSub.Brokers
{
    public class SimpleMessageBroker : MessageBroker
    {
        public override void AddSubscriber(Subscriber subscriber)
        {
            foreach (Topic topic in subscriber.Topics)
            {
                if (!SubscribersPerTopic.ContainsKey(topic))
                {
                    SubscribersPerTopic.Add(topic, new List<Subscriber>());
                }
                SubscribersPerTopic[topic].Add(subscriber);
                Trace.WriteLine(String.Format("Subsbcriber {0} Was Registered to broker {1}",subscriber.Id, Id));
            }
        }

        public override void PublishNewMessage(Message message)
        {
            List<Subscriber> subscribers = new List<Subscriber>();
            if (SubscribersPerTopic.TryGetValue(message.Topic, out subscribers))
            {
                subscribers.ForEach(subscriber => subscriber.ReadMessage(message));
            }
        }
    }
}
