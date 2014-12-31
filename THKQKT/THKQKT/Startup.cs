using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute("THKQKTConfiguration", typeof(THKQKT.Startup))]
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
