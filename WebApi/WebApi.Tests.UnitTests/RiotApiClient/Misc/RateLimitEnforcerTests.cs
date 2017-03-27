using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.RiotApiClient.Misc;
using WebApi.RiotApiClient.Misc.Interfaces;
using WebApi.Tests.Utilities;

namespace WebApi.Tests.UnitTests.RiotApiClient.Misc
{
    // TODO add TestCategory and execute on CI environment, include tests for minute limits

    [Ignore]
    [TestClass]
    public class RateLimitEnforcerTests
    {
        private static readonly TimeSpan MeasureTolerance = TimeSpan.FromMilliseconds(50);
        private const double ErrorFactor = 1.003;

        [TestMethod]
        public void GivenRateLimits_WhenSingleRequest_ThenEnforcementIsCorrect()
        {
            // arrange
            var stopWatch = new Stopwatch();

            // act
            stopWatch.Start();
            GetRateLimitEnforcer().EnforceRateLimitAsync().Wait();
            stopWatch.Stop();

            // assert
            AssertDelayed(stopWatch, TimeSpan.Zero);
        }

        [TestMethod]
        public void GivenRateLimits_WhenSecondLimitIsReached_ThenEnforcementIsCorrect()
        {
            // arrange
            var stopWatch = new Stopwatch();
            var rateLimitEnforcer = GetRateLimitEnforcer();

            // act
            stopWatch.Start();

            for (var i = 0; i < 10; i++)
            {
                rateLimitEnforcer.EnforceRateLimitAsync().Wait();
                Task.Delay(TimeSpan.FromSeconds(1)).Wait();
            }

            stopWatch.Stop();

            // assert
            AssertDelayed(stopWatch, TimeSpan.FromSeconds(10));
        }

        [TestMethod]
        public void GivenRateLimits_WhenSecondLimitIsExceeded_ThenEnforcementIsCorrect()
        {
            // arrange
            var stopWatch = new Stopwatch();
            var rateLimitEnforcer = GetRateLimitEnforcer();

            // act
            stopWatch.Start();

            for (var i = 0; i < 21; i++)
            {
                rateLimitEnforcer.EnforceRateLimitAsync().Wait();
                Task.Delay(TimeSpan.FromSeconds(1)).Wait();
            }

            stopWatch.Stop();

            // assert
            AssertDelayed(stopWatch, TimeSpan.FromSeconds(21));
        }

        [TestMethod]
        public void GivenRetryDelay_WhenApplyingRetryDelay_ThenEnforcementIsCorrect()
        {
            // arrange
            var stopWatch = new Stopwatch();
            var retryDelay = TimeSpan.FromSeconds(3);
            var rateLimitEnforcer = GetRateLimitEnforcer();

            // act
            stopWatch.Start();

            rateLimitEnforcer.SetRetryAfter(retryDelay);
            rateLimitEnforcer.EnforceRateLimitAsync().Wait();

            stopWatch.Stop();

            // assert
            AssertDelayed(stopWatch, retryDelay);
        }

        private static IRateLimitEnforcer GetRateLimitEnforcer()
        {
            return new RateLimitEnforcer(new TestApiKey());
        }

        private static void AssertDelayed(Stopwatch stopwatch, TimeSpan expectedDelay)
        {
            var actualDelay = stopwatch.Elapsed;

            Assert.IsTrue(
                expectedDelay < actualDelay.Add(MeasureTolerance),
                $"Too soon! Expected: {expectedDelay}. Actual: {actualDelay}.");

            Assert.IsTrue(
                actualDelay < TimeSpan.FromTicks((long)(expectedDelay.Ticks * ErrorFactor + MeasureTolerance.Ticks)),
                $"Too late! Expected: {expectedDelay}. Actual: {actualDelay}.");
        }
    }
}
