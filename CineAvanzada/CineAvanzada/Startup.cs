using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CineAvanzada.Startup))]
namespace CineAvanzada
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
