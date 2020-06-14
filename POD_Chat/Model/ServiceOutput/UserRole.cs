
using System.Collections.Generic;

namespace POD_Chat.Model.ServiceOutput
{
    public class UserRole
    {
        public long Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public List<string> Roles { get; set; }
    }
}
