using System;
using System.Collections.Generic;
using System.Text;

namespace Flyer.Extensions.Sequence
{
    /// <summary>
    /// TimeExtensions
    /// </summary>
    public static class TimeExtensions
    {
        /// <summary>
        /// currentTimeFunc
        /// </summary>
        public static Func<long> currentTimeFunc = InternalCurrentTimeMillis;

        private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        /// <summary>
        /// CurrentTimeMillis
        /// </summary>
        /// <returns></returns>
        public static long CurrentTimeMillis()
        {
            return currentTimeFunc();
        }
        /// <summary>
        /// StubCurrentTime
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IDisposable StubCurrentTime(Func<long> func)
        {
            currentTimeFunc = func;
            return new DisposableAction(() =>
            {
                currentTimeFunc = InternalCurrentTimeMillis;
            });
        }
        /// <summary>
        /// StubCurrentTime
        /// </summary>
        /// <param name="millis"></param>
        /// <returns></returns>
        public static IDisposable StubCurrentTime(long millis)
        {
            currentTimeFunc = () => millis;
            return new DisposableAction(() =>
            {
                currentTimeFunc = InternalCurrentTimeMillis;
            });
        }

        private static long InternalCurrentTimeMillis()
        {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }

    }
}
