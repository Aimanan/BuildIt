using System;
using System.Web;
using Buildit.Common.Providers.Contracts;

namespace Buildit.Common.Providers
{
    public class CacheProvider : ICacheProvider
    {
        private readonly HttpContextBase httpContext;

        public CacheProvider(HttpContextBase httpContext)
        {
            this.httpContext = httpContext;
        }

        public object GetValue(string key)
        {
            return this.httpContext.Cache[key];
        }

        public void InsertWithSlidingExpiration(string key, object value, int minutes)
        {
            this.httpContext.Cache.Insert(
                    key,
                    value,
                    null,
                    DateTime.MaxValue,
                    TimeSpan.FromMinutes(minutes));
        }

        public void InsertWithAbsoluteExpiration(string key, object value, DateTime absoluteExpiration)
        {
            this.httpContext.Cache.Insert(
                    key,
                    value,
                    null,
                    absoluteExpiration,
                    TimeSpan.Zero);
        }
    }
}