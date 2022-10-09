using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineJokes.Startup))]
namespace OnlineJokes
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
