namespace POD_Async.Model
{
    public enum AsyncMessageType
    {
        PING = 0,
        SERVER_REGISTER = 1,
        DEVICE_REGISTER = 2,
        MESSAGE = 3,
        MESSAGE_ACK_NEEDED = 4,
        MESSAGE_SENDER_ACK_NEEDED = 5,
        ACK = 6,
        PEER_REMOVED = -3,
        ERROR_MESSAGE = -99
    }
}
