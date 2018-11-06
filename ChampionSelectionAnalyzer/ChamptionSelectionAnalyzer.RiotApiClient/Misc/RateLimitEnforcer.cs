using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ChampionSelectionAnalyzer.RiotApiClient.Misc.Interfaces;
using NLog;

namespace ChampionSelectionAnalyzer.RiotApiClient.Misc
{
    // Information of Riot Developer API 
    // Rate Limiting: https://developer.riotgames.com/rate-limiting.html
    // API Keys: https://developer.riotgames.com/api-keys.html

    // Rate limits are enforced per region. 
    // For example, with the above rate limit, you could make 500 requests 
    // every 10 minutes to both NA and EUW endpoints simultaneously.

    // Rate Limits:
    // Development API Key: 20 requests every 1 second, 100 requests every 2 minutes
    // Production API Key: 3,000 requests every 10 seconds, 180,000 requests every 10 minutes

    /// <summary>
    /// This class is implemented as Singleton in order to ensure the Riot API Rate Limitation per region.
    /// </summary>
    public class RateLimitEnforcer : IRateLimitEnforcer
    {
        public static IRateLimitEnforcer GetRateLimitEnforcer(IApiKey apiKey, Region region)
        {
            var key = new Tuple<IApiKey, Region>(apiKey, region);
            if (!RateLimitEnforcersPerApiKeyAndRegion.ContainsKey(key))
            {
                RateLimitEnforcersPerApiKeyAndRegion.Add(key, new RateLimitEnforcer(key.Item1, key.Item2));
            }
            return RateLimitEnforcersPerApiKeyAndRegion[key];
        }

        // Singleton implemented according to https://msdn.microsoft.com/en-us/library/ff650316.aspx
        private static IDictionary<Tuple<IApiKey, Region>, IRateLimitEnforcer> RateLimitEnforcersPerApiKeyAndRegion
        {
            get
            {
                if (_rateLimitEnforcersPerApiKeyAndRegion != null)
                {
                    return _rateLimitEnforcersPerApiKeyAndRegion;
                }
                lock (SyncRoot)
                {
                    if (_rateLimitEnforcersPerApiKeyAndRegion == null)
                    {
                        _rateLimitEnforcersPerApiKeyAndRegion = new Dictionary<Tuple<IApiKey, Region>, IRateLimitEnforcer>();
                    }
                }

                return _rateLimitEnforcersPerApiKeyAndRegion;
            }
        }

        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private static volatile IDictionary<Tuple<IApiKey, Region>, IRateLimitEnforcer> _rateLimitEnforcersPerApiKeyAndRegion;
        private static readonly object SyncRoot = new object();
        
        private static readonly TimeSpan OneSecond = TimeSpan.FromSeconds(1);
        private static readonly TimeSpan TwoMinutes = TimeSpan.FromMinutes(2);

        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        private readonly int _limitPer1Sec;
        private readonly int _limitPer2Min;

        private int _numOfRequestsInLast1Sec;
        private int _numOfRequestsInLast2Min;

        private DateTime _firstRequestInLast1Sec = DateTime.MinValue;
        private DateTime _firstRequestInLast2Min = DateTime.MinValue;
        private DateTime _retryAfter = DateTime.MinValue;

        private RateLimitEnforcer(IApiKey apiKey, Region region)
        {
            Logger.Log(LogLevel.Info, $"Created {nameof(RateLimitEnforcer)} for API key { apiKey } and region { region }");
            SetRateLimitsForApiKey(apiKey, out _limitPer1Sec, out _limitPer2Min);
        }

        public async Task EnforceRateLimitAsync()
        {
            await _semaphore.WaitAsync();

            try
            {
                await Task.Delay(CalculateDelay());
                Update();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public void SetRetryAfter(TimeSpan retryDelay)
        {
            _retryAfter = DateTime.Now + retryDelay;
            Logger.Warn($"Retry-After Delay activated: {retryDelay:g}");
        }

        private static void SetRateLimitsForApiKey(IApiKey apiKey, out int limitPer1Sec, out int limitPer2Min)
        {
            if (apiKey.IsProduction)
            {
                limitPer1Sec = 300;
                limitPer2Min = 36000;
            }
            else
            {
                limitPer1Sec = 20;
                limitPer2Min = 100;
            }
        }

        private TimeSpan CalculateDelay()
        {
            var now = DateTime.Now;
            var delay = TimeSpan.Zero;

            // Check if any rate limit is reached
            if (_numOfRequestsInLast2Min >= _limitPer2Min)
            {
                var newDelay = _firstRequestInLast2Min + TwoMinutes - now;
                if (newDelay > delay)
                {
                    delay = newDelay;
                }
            }

            if (_numOfRequestsInLast1Sec >= _limitPer1Sec)
            {
                var newDelay = _firstRequestInLast1Sec + OneSecond - now;
                if (newDelay > delay)
                {
                    delay = newDelay;
                }
            }

            // Check if already punished with a RetryAfter
            var retryDelay = _retryAfter - now;
            if (retryDelay > delay)
            {
                delay = retryDelay;
            }

            if (delay > TimeSpan.Zero)
            {
                Logger.Warn($"Delaying request for {delay}");
            }

            return delay;
        }

        private void Update()
        {
            var now = DateTime.Now;

            if (_firstRequestInLast2Min < now - TwoMinutes)
            {
                _firstRequestInLast2Min = now;
                _numOfRequestsInLast2Min = 0;
            }

            if (_firstRequestInLast1Sec < now - OneSecond)
            {
                _firstRequestInLast1Sec = now;
                _numOfRequestsInLast1Sec = 0;
            }

            _numOfRequestsInLast2Min++;
            _numOfRequestsInLast1Sec++;
        }
    }
}
