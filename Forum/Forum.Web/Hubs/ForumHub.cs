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
        public void Notify(string userName)
        {           
            var messageCount = 0; // GetMessageCount(userName);

            Clients.All.notifyMessageCount(messageCount);
        }

    }
}