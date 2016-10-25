using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Puma.Prey.Raccoon.Startup))]
namespace Puma.Prey.Raccoon
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
