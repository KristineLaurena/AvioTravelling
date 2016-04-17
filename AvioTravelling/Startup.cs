using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AvioTravelling.Startup))]
namespace AvioTravelling
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
