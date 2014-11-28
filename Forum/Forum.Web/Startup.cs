using Forum.Web.Hubs;
using Microsoft.AspNet.SignalR;
using Owin;

namespace IdentitySample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            app.MapSignalR();
        }
    }
}
