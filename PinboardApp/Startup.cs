using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PinboardApp.Startup))]
namespace PinboardApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
