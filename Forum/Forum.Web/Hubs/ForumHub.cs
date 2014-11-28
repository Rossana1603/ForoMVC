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
using System.Security.Principal;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace Forum.Web.Hubs
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(IRequest request)
        {
            return request.User.Identity.GetUserId();
        }
    }

    public class ConnectionUser
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
    }

    public class ForumHub : Hub
    {
        private static volatile IHubContext _connectionManager;
        private static readonly Object LockerInstance = new Object();
        private static Dictionary<string, string> _connectionByUsersDictionary = new Dictionary<string, string>();

        /// <summary>
        /// Singleton pattern property. It returns an instance of the IHubContext interface.
        /// </summary>       
        public static IHubContext ConnectionManager
        {
            get
            {
                if (_connectionManager == null)
                {
                    lock (LockerInstance)
                    {
                        if (_connectionManager == null)
                            _connectionManager = GlobalHost.ConnectionManager.GetHubContext<ForumHub>();
                    }
                }

                return _connectionManager;
            }
        }

        public static Dictionary<string, string> ConnectionByUsersDictionary
        {
            get {
                return _connectionByUsersDictionary;
                }
        }


        public bool Send(string userName, string message)
        {
            var connectionIds = ConnectionByUsersDictionary.Where(x => x.Value == userName);

            foreach (var connectionId in connectionIds)
            {
                if (ConnectionManager.Clients.Client(connectionId.Key) != null)
                {
                    ConnectionManager.Clients.Client(connectionId.Key).notifyOnlineUser(message);
                }
            }

            return true;
        }

        ///
        /// register online user
        ///
        ///
        public override Task OnConnected()
        {
            var connectionId = Context.ConnectionId;
            var userName = Context.User.Identity.GetUserName();

            if (!string.IsNullOrEmpty(userName))
            {
                ConnectionByUsersDictionary.Add(connectionId, userName);
            }            

            return base.OnConnected();   
        }

        ///
        /// unregister disconected user
        ///
        ///
        public override Task OnDisconnected(bool stopCalled)
        {
            var connectionId = Context.ConnectionId;
            var userName = Context.User.Identity.GetUserName();

            if (!string.IsNullOrEmpty(userName))
            {
                ConnectionByUsersDictionary.Remove(connectionId);
            }

            return base.OnConnected();
        }
    }
}