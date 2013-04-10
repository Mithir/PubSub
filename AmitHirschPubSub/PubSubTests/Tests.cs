using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PubSub.Subscribers;
using PubSub.Brokers;
using PubSub.Messages;
using PubSub.Publishers;

namespace PubSubTests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Subscriber_With_OneTopic_Register_Broker_ShouldHave_SubscriberAndTopic()
        {
            List<Topic> topics = new List<Topic> { Topic.Oil };
            SimpleSubscriber subscriber = new SimpleSubscriber { Topics = topics, Id = 1 };
            SimpleMessageBroker broker = new SimpleMessageBroker();

            subscriber.Register(broker);

            Assert.IsTrue(broker.SubscribersPerTopic.ContainsKey(Topic.Oil));
            Assert.IsTrue(broker.SubscribersPerTopic[Topic.Oil].Exists(x => x.Id == 1));
        }

        [TestMethod]
        public void Subscriber_With_TwoTopic_Register_Broker_ShouldHave_SubscriberAndTopics()
        {
            List<Topic> topics = new List<Topic> { Topic.Oil, Topic.Dollar };
            SimpleSubscriber subscriber = new SimpleSubscriber { Topics = topics, Id = 1 };
            SimpleMessageBroker broker = new SimpleMessageBroker();

            subscriber.Register(broker);

            topics.ForEach(topic =>
                {
                    Assert.IsTrue(broker.SubscribersPerTopic.ContainsKey(topic));
                    Assert.IsTrue(broker.SubscribersPerTopic[topic].Exists(x => x.Id == 1));
                });
        }

        [TestMethod]
        public void Various_Subscribers_With_Topics_Register_Broker_ShouldHave_SubscribersAndTopics()
        {
            List<Topic> topics = new List<Topic> { Topic.Oil, Topic.Dollar };
            List<Subscriber> subscribers = new List<Subscriber>
            { 
                new SimpleSubscriber { Topics = topics, Id = 1 },
                new SimpleSubscriber { Topics = topics, Id = 2 },
                new SimpleSubscriber { Topics = topics, Id = 3 }
            };

            SimpleMessageBroker broker = new SimpleMessageBroker();

            subscribers.ForEach(subscriber =>subscriber.Register(broker));

            topics.ForEach(topic =>
            {
                Assert.IsTrue(broker.SubscribersPerTopic.ContainsKey(topic));
                subscribers.ForEach(subscriber => broker.SubscribersPerTopic[topic].Exists(x => x.Id == subscriber.Id));
            });
        }

        [TestMethod]
        public void Test_Subscribers_With_Topics_Register_Broker_ShouldHave_SubscribersAndTopics()
        {
            List<Topic> topics = new List<Topic> { Topic.Oil, Topic.Dollar };
            List<Subscriber> subscribers = new List<Subscriber>
            { 
                new TestSubscriber { Topics = topics, Id = 1 },
                new TestSubscriber { Topics = topics, Id = 2 },
                new TestSubscriber { Topics = topics, Id = 3 }
            };

            SimpleMessageBroker broker = new SimpleMessageBroker();

            subscribers.ForEach(subscriber => subscriber.Register(broker));

            topics.ForEach(topic =>
            {
                Assert.IsTrue(broker.SubscribersPerTopic.ContainsKey(topic));
                subscribers.ForEach(subscriber => broker.SubscribersPerTopic[topic].Exists(x => x.Id == subscriber.Id));
            });
        }

        [TestMethod]
        public void Test_Subscribers_ReadPublishMessages_OnlyOil()
        {
            List<Topic> topicsOil = new List<Topic> { Topic.Oil};
            List<Topic> topicsDollar = new List<Topic> { Topic.Dollar };
            List<Subscriber> subscribers = new List<Subscriber>
            { 
                new TestSubscriber { Topics = topicsOil, Id = 1 },
                new TestSubscriber { Topics = topicsOil, Id = 2 },
                new TestSubscriber { Topics = topicsDollar, Id = 3 }
            };
            SimpleMessageBroker broker = new SimpleMessageBroker();
            SimplePublisher publisher = new SimplePublisher { Id = 1 };

            subscribers.ForEach(subscriber => subscriber.Register(broker));

            topicsOil.ForEach(topic =>
            {
                Assert.IsTrue(broker.SubscribersPerTopic.ContainsKey(topic));
                subscribers.ForEach(subscriber => broker.SubscribersPerTopic[topic].Exists(x => x.Id == subscriber.Id));
            });

            topicsDollar.ForEach(topic =>
            {
                Assert.IsTrue(broker.SubscribersPerTopic.ContainsKey(topic));
                subscribers.ForEach(subscriber => broker.SubscribersPerTopic[topic].Exists(x => x.Id == subscriber.Id));
            });

            publisher.Publish(broker, new Message { Id = 1, Text = "This is a TestMessage", Topic = Topic.Oil });
            broker.SubscribersPerTopic[Topic.Oil].ForEach(subcriber =>
                {
                    Assert.IsTrue((subcriber as TestSubscriber).MessageWasRead);
                });

            broker.SubscribersPerTopic[Topic.Dollar].ForEach(subcriber =>
            {
                Assert.IsFalse((subcriber as TestSubscriber).MessageWasRead);
            });
        }

        [TestMethod]
        public void MyTests()
        {
            List<Topic> topics = new List<Topic> { Topic.Oil, Topic.Dollar };
            List<Subscriber> subscribers = new List<Subscriber>
            { 
                new SimpleSubscriber { Topics = topics, Id = 1 },
                new SimpleSubscriber { Topics = topics, Id = 2 },
                new SimpleSubscriber { Topics = topics, Id = 3 }
            };
            SimplePublisher publisher = new SimplePublisher { Id = 1 };
            SimpleMessageBroker broker = new SimpleMessageBroker();

            subscribers.ForEach(subscriber => subscriber.Register(broker));

            topics.ForEach(topic =>
            {
                Assert.IsTrue(broker.SubscribersPerTopic.ContainsKey(topic));
                subscribers.ForEach(subscriber => broker.SubscribersPerTopic[topic].Exists(x => x.Id == subscriber.Id));
            });

            publisher.Publish(broker, new Message { Id = 1, Text = "This is a TestMessage", Topic = Topic.Oil });
        }
    }
}
