namespace POD_Async.Model
{
    public class ClientMessage
    {
        public long id { get; set; }

        /// <summary>
        /// type of message
        /// </summary>
        public AsyncMessageType type { get; set; }
        public long senderMessageId { get; set; }

        /// <summary>
        /// hows that the message was received from which device(Peer id)
        /// </summary>
        public long senderId { get; set; }

        /// <summary>
        /// Id generate by platform (if message sent in sync mode)
        /// </summary>
        public long trackerId { get; set; }
        public string senderName { get; set; }
        public string content { get; set; }

        /// <summary>
        /// address of sender (if sender registered on your peer)
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// origin of sender (if sender registered on your peer)
        /// </summary>
        public string origin { get; set; }
    }
}
