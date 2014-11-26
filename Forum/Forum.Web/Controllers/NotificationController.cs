using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Forum.Persistence.DataAccess;
using Forum.Persistence.Domain;
using Forum.Web.Hubs;
using Microsoft.Ajax.Utilities;
using RestSharp;
using Forum.Web.Properties;
using AutoMapper;

namespace Forum.Web.Controllers
{
    public class NotificationController : CustomControllerBase
    {
        public ActionResult Notify(int topicId)
        {
            var client = new RestClient(Settings.Default.ForumApiUrl);
            var request = new RestRequest("api/Subscription/GetSubscriptorsByTopicId/{topicId}", Method.GET);
            request.AddParameter("topicId", topicId);
            var lstSubscribers = client.Execute<List<Subscription>>(request).Data;

            var lstNotifyUsers = GetNotifyUsers(lstSubscribers);

            var lstExceptNotifyUsers = lstSubscribers.Except(lstNotifyUsers).ToList();

            var forumHub = new ForumHub();

            forumHub.Send(lstNotifyUsers);

            return View();

        }


        private List<Subscription> GetNotifyUsers(List<Subscription> lstSubscribers )
        {
            var onlineUsers = ForumHub.connectionByUsersDictionary.Values.ToList();
            var lstNotifyUsers = new List<Subscription>();

            foreach (var userName in onlineUsers)
            {
                var subscriberConnected = lstSubscribers.FirstOrDefault(x => x.Author.UserName == userName);
                if (subscriberConnected != null)
                {
                    lstNotifyUsers.Add(subscriberConnected);
                }
            }

            return lstNotifyUsers;
        }
	}
}