using System;
using System.Net;

namespace WebApi.RiotApiClient.Misc.Exceptions
{
    public class RioApiException : Exception
    {
        public readonly HttpStatusCode HttpStatusCode;

        public RioApiException(string message)
            : base(message)
        {
            
        }

        public RioApiException(string message, HttpStatusCode httpStatusCode) 
            : this(message)
        {
            HttpStatusCode = httpStatusCode;
        }
    }
}
