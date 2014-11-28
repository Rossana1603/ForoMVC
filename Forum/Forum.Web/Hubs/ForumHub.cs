using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using System.Web.Mvc.Ajax;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR.Messaging;
using Microsoft.AspNet.SignalR.Owin;
using Domain = Forum.Persistence.Domain;
using System.Security.Principal;

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
        public static Dictionary<string,string> connectionByUsersDictionary = new Dictionary<string, string>();
        public static Dictionary<string, ConnectionUser> connectionByUsersIdDictionary = new Dictionary<string, ConnectionUser>();

        //public bool Send(string userName, string message)
        //{
        //    var user = connectionByUsersIdDictionary.Values.Where(x => x.UserName == userName).FirstOrDefault();
        //    var connectionIds = connectionByUsersIdDictionary.Keys.Where(key => connectionByUsersIdDictionary[key].UserName == userName)
        //                        .ToList()
        //                        .ToArray();

        public bool Send(string userName, string message)
        {
            var connectionId = connectionByUsersDictionary.FirstOrDefault(x => x.Value == userName);
            Clients.User(connectionId.Key).notifyOnlineUser(message);

            return true;
        }
        //    foreach (var connectionId in connectionIds)
        //    {
        //        if (Clients.Client(connectionId)!=null)
        //        {
        //            Clients.Client(connectionId).notifyOnlineUser(message);
        //        }                
        //    }
                

        //    return true;
        //}

        ///
        /// register online user
        ///
        ///
        public override System.Threading.Tasks.Task OnConnected()
        {
            var user = Context.User.Identity;

            if (!string.IsNullOrEmpty(user.GetUserName()))
            {
                connectionByUsersDictionary.Add(Context.ConnectionId, user.GetUserName());

                connectionByUsersIdDictionary.Add(Context.ConnectionId, new ConnectionUser { UserId = user.GetUserId(), UserName = user.GetUserName() });
            }            

            return base.OnConnected();   
        }

        ///
        /// unregister disconected user
        ///
        ///
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            if (!string.IsNullOrEmpty(Context.User.Identity.GetUserName()))
            {
                connectionByUsersDictionary.Remove(Context.ConnectionId);

                connectionByUsersIdDictionary.Remove(Context.ConnectionId);
            }

            return base.OnConnected();

        }
    }
}