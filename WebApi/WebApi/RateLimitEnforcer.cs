using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{
    /// <summary>
    /// Rate Limit(s):
    /// * 10 requests every 10 seconds 
    /// * 500 requests every 10 minutes 
    /// Note that rate limits are enforced per region. 
    /// For example, with the above rate limit, you could make 500 requests 
    /// every 10 minutes to both NA and EUW endpoints simultaneously.
    /// </summary>
    public class RateLimitEnforcer
    {
    }
}
