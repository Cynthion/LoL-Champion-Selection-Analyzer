using System;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Misc.Interfaces;

namespace WebApi.Misc
{
    // Information of Riot Developer API 
    // Rate Limiting: https://developer.riotgames.com/rate-limiting.html
    // API Keys: https://developer.riotgames.com/api-keys.html

    // TODO allow region swap
    // Rate limits are enforced per region. 
    // For example, with the above rate limit, you could make 500 requests 
    // very 10 minutes to both NA and EUW endpoints simultaneously.

    // Rate Limits:
    // Development API Key: 10 requests every 10 seconds, 500 requests every 10 minutes
    // Production API Key: 3,000 requests every 10 seconds, 180,000 requests every 10 minutes

    public class RateLimitEnforcer : IRateLimitEnforcer
    {
        private static readonly TimeSpan TenSeconds = TimeSpan.FromSeconds(10);
        private static readonly TimeSpan TenMinutes = TimeSpan.FromMinutes(10);

        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        private readonly int _limitPer10Sec;
        private readonly int _limitPer10Min;

        private int _numOfRequestsInLast10Sec;
        private int _numOfRequestsInLast10Min;

        private DateTime _firstRequestInLast10Sec = DateTime.MinValue;
        private DateTime _firstRequestInLast10Min = DateTime.MinValue;
        private DateTime _retryAfter = DateTime.MinValue;

        public RateLimitEnforcer(RiotApiKey apiKey)
        {
            SetRateLimitsForApiKey(apiKey, out _limitPer10Sec, out _limitPer10Min);
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
        }

        private static void SetRateLimitsForApiKey(RiotApiKey apiKey, out int limitPer10Sec, out int limitPer10Min)
        {
            if (apiKey.IsProduction)
            {
                limitPer10Sec = 10;
                limitPer10Min = 500;
            }
            else
            {
                limitPer10Sec = 3000;
                limitPer10Min = 180000;
            }
        }

        private TimeSpan CalculateDelay()
        {
            var now = DateTime.Now;
            var delay = TimeSpan.Zero;

            // Check if any rate limit is reached
            if (_numOfRequestsInLast10Min >= _limitPer10Min)
            {
                var newDelay = _firstRequestInLast10Min + TenMinutes - now;
                if (newDelay > delay)
                {
                    delay = newDelay;
                }
            }

            if (_numOfRequestsInLast10Sec >= _limitPer10Sec)
            {
                var newDelay = _firstRequestInLast10Sec + TenSeconds - now;
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

            return delay;
        }

        private void Update()
        {
            var now = DateTime.Now;

            if (_firstRequestInLast10Min < now - TenMinutes)
            {
                _firstRequestInLast10Min = now;
                _numOfRequestsInLast10Min = 0;
            }

            if (_firstRequestInLast10Sec < now - TenSeconds)
            {
                _firstRequestInLast10Sec = now;
                _numOfRequestsInLast10Sec = 0;
            }

            _numOfRequestsInLast10Min++;
            _numOfRequestsInLast10Sec++;
        }
    }
}
