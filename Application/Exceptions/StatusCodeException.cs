using System;
using System.Net;

namespace ProjectAPI
{
    public class StatusCodeException : Exception
    {
        public HttpStatusCode Code { get; set; }
        public string Error { get; set; }

        public StatusCodeException(HttpStatusCode Code, string Error) : base(Error)
        {
            this.Code = Code;
            this.Error = Error;
        }
    }
}
