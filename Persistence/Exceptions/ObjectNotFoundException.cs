using System;
using System.Net;

namespace Application.Exceptions
{
    public class ObjectNotFoundException : Exception
    {
        public HttpStatusCode Code { get; set; }
        public string Error { get; set; }

        public ObjectNotFoundException(string Error) : base(Error)
        {
            this.Code = HttpStatusCode.NotFound;
            this.Error = Error;
        }
    }
}
