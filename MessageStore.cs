using System;
using System.Collections.Generic;

namespace XMPP
{
    public class MessageStore
    {
        public static List<string> RcvdMessages { get; set; }

        public static List<string> SentMessages { get; set; }

        public static List<string> DelayedMessages { get; set; }
    }
}

