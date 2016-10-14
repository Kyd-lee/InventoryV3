using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InventoryV3.Startup))]
namespace InventoryV3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
