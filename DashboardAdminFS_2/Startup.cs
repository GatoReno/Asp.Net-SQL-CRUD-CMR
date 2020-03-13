using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DashboardAdminFS_2.Startup))]
namespace DashboardAdminFS_2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
