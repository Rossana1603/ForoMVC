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
    public class ForumHub : Hub
    {
        public static Dictionary<string,string> connectionByUsersDictionary = new Dictionary<string, string>();

        public void Send()
        {
            string message = "";
            // Call the recalculateOnlineUsers method to update clients
            if (connectionByUsersDictionary.Count < 2)
                message = string.Format("Currently {0} user is online.", connectionByUsersDictionary.Count);
            else
                message = string.Format("Currently {0} users are online.", connectionByUsersDictionary.Count);

            Clients.All.recalculateOnlineUsers(message);

        }

        public void Send(List<Domain.Subscription> NotifyUsers)
        {
            string message = "";

            Clients.All.notifyOnlineUsers(message);
            foreach (var client in NotifyUsers)
            {
                //TODO: will be researched 
            }
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