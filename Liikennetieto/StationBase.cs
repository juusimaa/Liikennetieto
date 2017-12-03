using System;
using System.Runtime.Caching;
using Utility.Wpf;

namespace Liikennetieto
{
    internal abstract class StationBase : BindingBase
    {
        protected ObjectCache cache;
        protected DateTime startTime;
        protected CacheItemPolicy policy;
        protected TimeSpan cacheTimeout = TimeSpan.FromHours(1.0);

        public StationBase()
        {
            policy = new CacheItemPolicy();
            cache = MemoryCache.Default;
            startTime = DateTime.Now;
        }
    }
}
