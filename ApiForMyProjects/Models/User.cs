using System;
using System.Collections.Generic;

namespace ApiForMyProjects.Models
{
    public partial class User
    {
        public long IntUserId { get; set; }
        public string StrUserName { get; set; }
        public string StrEmail { get; set; }
        public string StrPassword { get; set; }
        public DateTime DteCreatedAt { get; set; }
        public DateTime DteUpdatedAt { get; set; }
    }
}
