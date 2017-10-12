using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BaseFramework.Startup))]
namespace BaseFramework
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
