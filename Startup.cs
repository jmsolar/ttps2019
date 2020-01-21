using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MercadoVentasTP.Startup))]
namespace MercadoVentasTP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
