
namespace POD_Chat.Model.ServiceOutput
{
    public class UploadToPodSpaceResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string HashCode { get; set; }
        public long Created { get; set; }
        public long Size { get; set; }
        public string Type { get; set; }
        public string ParentHash { get; set; }
    }
}
