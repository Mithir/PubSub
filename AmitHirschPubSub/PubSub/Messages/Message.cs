using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PubSub.Messages
{
    public class Message
    {
        public Topic Topic { get; set; }

        public String Text { get; set; }

        public int Id { get; set; }

    }
}
