using System;
using System.Net;

namespace WebApi.RiotApiClient.Misc.Exceptions
{
    public class RiotApiException : Exception
    {
        public readonly HttpStatusCode HttpStatusCode;

        public RiotApiException(string message)
            : base(message)
        {
            
        }

        public RiotApiException(string message, HttpStatusCode httpStatusCode) 
            : this(message)
        {
            HttpStatusCode = httpStatusCode;
        }
    }
}
