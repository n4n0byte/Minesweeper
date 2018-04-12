using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Minesweeper.Startup))]
namespace Minesweeper
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
