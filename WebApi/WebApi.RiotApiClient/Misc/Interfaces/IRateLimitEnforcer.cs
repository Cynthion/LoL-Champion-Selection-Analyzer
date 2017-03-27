using System;
using System.Threading.Tasks;

namespace WebApi.RiotApiClient.Misc.Interfaces
{
    public interface IRateLimitEnforcer
    {
        /// <summary>
        /// Blocks until a request is allowed.
        /// </summary>
        Task EnforceRateLimitAsync();

        /// <summary>
        /// Provides a RetryAfter delay from a 429 status code.
        /// </summary>
        void SetRetryAfter(TimeSpan retryDelay);
    }
}
