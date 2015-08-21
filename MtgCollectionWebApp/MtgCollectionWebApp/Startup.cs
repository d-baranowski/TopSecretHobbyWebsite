using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MtgCollectionWebApp.Startup))]
namespace MtgCollectionWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
