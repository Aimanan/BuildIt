using Buildit.Common.Providers.Contracts;
using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Buildit.Common.Providers
{
    public class ServerProvider : IServerProvider
    {
        private readonly HttpContextBase httpContext;

        public ServerProvider(HttpContextBase httpContext)
        {
            Guard.WhenArgument(httpContext, "HttpContext").IsNull().Throw();

            this.httpContext = httpContext;
        }

        public string MapPath(string relativePath)
        {
            return this.httpContext.Server.MapPath(relativePath);
        }
    }
}