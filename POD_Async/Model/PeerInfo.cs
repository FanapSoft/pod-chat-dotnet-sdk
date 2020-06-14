namespace POD_Async.Model
{
    public class PeerInfo
    {
        public string deviceId { get; set; }

        /// <summary>
        /// Id of your application
        /// </summary>
        public string appId { get; set; }

        /// <summary>
        /// When the client disconnected by set this true
        /// </summary>
        public bool refresh { get; set; }
        public bool renew { get; set; }
    }
}
