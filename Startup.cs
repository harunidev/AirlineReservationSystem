using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectFinal.Startup))]
namespace ProjectFinal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
