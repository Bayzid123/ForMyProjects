using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiForMyProjects.Helper
{
    public class Message
    {
        [DataMember]
        public bool status { get; set; }
        public string message { get; set; }
        public object data { get; set; }
        [JsonIgnore]
        public string errors { get; set; }

        
    }
}
