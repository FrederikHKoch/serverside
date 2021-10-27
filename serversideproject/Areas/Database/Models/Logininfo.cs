using System;
using System.Collections.Generic;

namespace serversideproject.Areas.Database.Models
{
    public partial class Logininfo
    {
        public int Id { get; set; }
        public string User { get; set; } = null!;
        public string Password { get; set; } = null!;
        public byte[]? Salt { get; set; }
    }
}
