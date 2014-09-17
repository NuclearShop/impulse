using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ImpulseApp.Startup))]
namespace ImpulseApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
