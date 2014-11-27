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

        public void ProcessNotifications(Post post)
        {
            var subscriptionController = new SubscriptionController();

            var subscribers = subscriptionController.GetSubcriptionsByTopicId(post.TopicId);

            var message = post.Content.ToContentPreview(post.UserName);

            foreach (var subscriber in subscribers)
            {
                AddNotification(subscriber.Id, post.Id, message);
                SendNotifications(subscribers, message);
            }
        }

        private void AddNotification(int subscriptionId, int postId, string message)
        {
            var client = new RestClient(Settings.Default.ForumApiUrl + "api/notification/");
            var request = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };
            request.AddJsonBody(new Notification()
            {
                SubscriptionId = subscriptionId,
                NotificationDate = DateTime.Now,
                PostId = postId,
                State = State.Pending,
                Message = message
            });
            var response = client.Execute<Notification>(request);
        }

        private void SendNotifications(List<Subscription> subscribers, string message)
        {
            var notifyUsers = GetNotifyUsers(subscribers);

            var forumHub = new ForumHub();

            foreach (var subscriber in notifyUsers)
                forumHub.Send(subscriber.Author.UserName, message);

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
    }
}