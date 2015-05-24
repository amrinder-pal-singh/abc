using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(abc.Startup))]
namespace abc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
