using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using System.Web.Mvc.Ajax;
using Microsoft.AspNet.Identity;

namespace Forum.Web.Hubs
{
    public class ForumHub : Hub
    {
        /// <summary>
        /// Information about users to be notifiy
        /// </summary>
        /// <param name="userName"></param>
        public void NotifyNewPost(string userName)
        {           
            var messageCount = 10; // GetMessageCount(userName);

            Clients.All.notifyNewPostMessageCount(messageCount);
        }
    }
}