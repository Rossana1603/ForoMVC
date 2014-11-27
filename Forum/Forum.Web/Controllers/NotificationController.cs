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
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using Forum.Web.Resolver;

namespace Forum.Web.Controllers
{
    public class NotificationController : CustomControllerBase
    {
        public void Notify(List<Subscription> subscribers, int topicId)
        {
            var notifyUsers = GetNotifyUsers(subscribers);

            var forumHub = new ForumHub();

            foreach (var subscriber in subscribers)
                forumHub.Send(subscriber.Author.UserName, subscriber.Message);

        }

        private List<Subscription> GetNotifyUsers(List<Subscription> subscribers)
        {
            var onlineUsers = ForumHub.connectionByUsersDictionary.Values.ToList();
            var notifyUsers = new List<Subscription>();

            foreach (var userName in onlineUsers)
            {
                var subscriberConnected = subscribers.FirstOrDefault(x => x.Author.UserName == userName);
                if (subscriberConnected != null)
                {
                    notifyUsers.Add(subscriberConnected);
                }
            }

            return notifyUsers;
        }

        public void AddNotification(List<Subscription> subscribers, int postId, string content)
        {
            foreach (var subscriber in subscribers)
                AddNotification(subscriber.Id, postId, content.ToContentPreview());
        }

        private void AddNotification(int subscriptionId, int postId, string contentPreview)
        {
            var client = new RestClient(Settings.Default.ForumApiUrl + "api/notification/");
            var request = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };
            request.AddJsonBody(new Notification()
            {
                SubscriptionId = subscriptionId,
                Subscription = null,
                PostId = postId,
                Post = null,
                NotificationDate = null,
                State = State.Pending,
                Message = contentPreview
            });
            var response = client.Execute<Post>(request);
        }
    }
}