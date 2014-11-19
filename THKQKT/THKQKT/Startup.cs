using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(THKQKT.Startup))]
namespace THKQKT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
