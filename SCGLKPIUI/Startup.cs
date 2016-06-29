using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SCGLKPIUI.Startup))]
namespace SCGLKPIUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
