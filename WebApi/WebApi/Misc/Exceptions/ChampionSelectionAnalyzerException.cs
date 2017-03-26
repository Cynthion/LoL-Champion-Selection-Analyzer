using System;
using System.Net;

namespace WebApi.Misc.Exceptions
{
    public class ChampionSelectionAnalyzerException : Exception
    {
        public readonly HttpStatusCode HttpStatusCode;

        public ChampionSelectionAnalyzerException(string message, HttpStatusCode httpStatusCode) 
            : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }
    }
}
