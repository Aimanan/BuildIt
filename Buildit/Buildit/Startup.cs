using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Buildit.Startup))]
namespace Buildit
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
