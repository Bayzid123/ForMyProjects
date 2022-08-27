using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiForMyProjects.Helper
{
    public class HttpRequest
    {
        public async Task<string> GetResult(string uri)
        {
            Uri myUri = new Uri(uri);

            WebRequest myWebRequest = WebRequest.Create(myUri);
            WebResponse myWebResponse = myWebRequest.GetResponse();

            return myWebResponse.ToString();

        }
    }
}
