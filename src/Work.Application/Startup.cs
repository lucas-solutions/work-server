using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lucas.Solutions.Startup))]
namespace Lucas.Solutions
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
