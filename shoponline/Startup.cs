using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(shoponline.Startup))]
namespace shoponline
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
