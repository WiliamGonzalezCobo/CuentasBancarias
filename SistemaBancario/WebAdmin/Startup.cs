using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAdmin.Startup))]
namespace WebAdmin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
