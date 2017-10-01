using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AnimalsShelter.Web.Startup))]
namespace AnimalsShelter.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
