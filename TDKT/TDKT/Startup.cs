using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute("TDKTConfiguration", typeof(TDKT.Startup))]
namespace TDKT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
