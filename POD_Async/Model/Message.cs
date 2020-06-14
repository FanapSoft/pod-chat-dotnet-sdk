using System;

namespace POD_Async.Model
{
    public class Message
    {
        /// <summary>
        /// name of receiver peer
        /// </summary>
        public string peerName { get; set; }
        public string content { get; set; }

        /// <summary>
        /// array of receiver peer ids (if you use this, peerName will be ignored)
        /// </summary>
        public long[] receivers { get; set; }

        /// <summary>
        /// priority of message 1-10, lower has more priority
        /// </summary>
        public int priority { get; set; }
        public long messageId { get; set; }

        /// <summary>
        /// time to live for message in millisecond
        /// </summary>
        public long ttl { get; set; }
    }
}
