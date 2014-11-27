using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using System.Web.Mvc.Ajax;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR.Messaging;
using Domain = Forum.Persistence.Domain;

namespace Forum.Web.Hubs
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(IRequest request)
        {
            return request.User.Identity.Name;
        }
    }

    public class ForumHub : Hub
    {
        public static Dictionary<string,string> connectionByUsersDictionary = new Dictionary<string, string>();


        public bool Send(string userName, string message)
        {
            Clients.User(userName).notifyOnlineUser(message);

            return true;
        }

        ///
        /// register online user
        ///
        ///
        public override System.Threading.Tasks.Task OnConnected()
        {
            var userName = Context.User.Identity.Name;

            connectionByUsersDictionary.Add(Context.ConnectionId, userName);          

            return base.OnConnected();   
        }

        ///
        /// unregister disconected user
        ///
        ///
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            connectionByUsersDictionary.Remove(Context.ConnectionId); 
         
            return base.OnDisconnected(stopCalled);
        }
    }
}