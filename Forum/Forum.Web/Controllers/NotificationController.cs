using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Forum.Persistence.Domain;
using Forum.Web.Models;
using Forum.Web.Properties;
using Microsoft.AspNet.Identity;
using RestSharp;
using PagedList;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Forum.Web.Hubs;
using Forum.Web.Resolver;

namespace Forum.Web.Controllers
{
    [Authorize]
    public class NotificationController : CustomControllerBase
    {
        /// <summary>
        /// This method will run asynchrounusly. an awaited task is needed to 
        /// place the notification in a background process
        /// </summary>
        /// <param name="post"></param>
        public async void ProcessNotifications(Post post)
        {
            await Task.Run(() =>
            {
                var subscriptionController = new SubscriptionController();

            	var subscribers = subscriptionController.GetSubcriptionsByTopicId(post.TopicId);

            	var message = post.Content.ToContentPreview(post.UserName);
                #region delay emulation
                    //////////TODO: erase
                    //////////only for testing purposes:begin
                    //////////this is actually emulatting a delay
                    //var n = 1.00;
                    //while (n < 9.95)
                    //{
                    //    n = new Random().NextDouble() * 10;
                    //}
                    //////////only for testing purposes:end
                #endregion

                foreach (var subscriber in subscribers)
                {
                    AddNotification(subscriber.Id, post.Id, message);
                    SendNotifications(subscribers, message);
                }
            });
        }

        public void SubscriptionNotification(Subscription subscription)
        {
                var topicController = new TopicController();
                var topic = topicController.GetTopic(subscription.TopicId);
                var message = string.Format("Se ah subscrito al siguiente topic: {0} ", topic.Title);
                AddNotification(subscription.Id, null, message);
        }

        public void AddNotification(int subscriptionId, int? postId, string message)
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
            {
                forumHub.Send(subscriber.Author.UserName, message);                
            }

        }

        private List<Subscription> GetNotifyUsers(List<Subscription> subscribers)
        {
            var onlineUsers = ForumHub.ConnectionByUsersDictionary.Values.ToList();
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

        public void DeletePostNotification(int postId)
        {
            var client = new RestClient(Settings.Default.ForumApiUrl + "api/Notification/DeleteByPostId/{postId}");
            var request = new RestRequest(Method.DELETE);
            request.AddParameter("postId", postId);

            var response = client.Execute<Notification>(request);
        }

        public void DeleteSubscriptionNotification(int subscriptionId)
        {
            var client = new RestClient(Settings.Default.ForumApiUrl + "api/Notification/DeleteBySubscriptionId/{subscriptionId}");
            var request = new RestRequest(Method.DELETE);
            request.AddParameter("subscriptionId", subscriptionId);

            var response = client.Execute<Notification>(request);
        }
    }
}