using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartnerManagement.Helper
{
    public class DynamicMessage<t>
    {  
        public bool status { get; set; }
        public string message { get; set; }
        public t data { get; set; } 
        public string errors { get; set; }

        public static async Task<DynamicMessage<t>> ReturnMessage(dynamic data,string error,bool status,string msg)
        { 
           var  mesg= new  DynamicMessage<t>
            {
                status = status,
                message = msg,
                data = data,
                errors = error
            };
            return mesg;
        }
    }
}
