using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ApiForMyProjects.Helper
{
    public class MessageHelper
    {
        public MessageHelper() { }
        public MessageHelper(string message, long status)
        {
            Message = message; statuscode = status;
        }
        public MessageHelper(string message, string error, long status) : this(message, status)
        {
            Error = error;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public long statuscode { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
    }
}
