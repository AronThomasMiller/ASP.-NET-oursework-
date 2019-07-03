using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LibraryCursova.Startup))]
namespace LibraryCursova
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
