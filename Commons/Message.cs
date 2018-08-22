using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commons
{
    public class Message
    {
        public Guid ID { get; set; }

        public string Receipient { get; set; }

        public string Content { get; set; }
    }
}
