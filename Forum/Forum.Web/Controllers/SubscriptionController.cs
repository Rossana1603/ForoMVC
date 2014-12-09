using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Forum.Persistence.Domain;
using Forum.Web.Models;
using Forum.Web.Properties;
using IdentitySample.Models;
using Microsoft.AspNet.Identity;
using RestSharp;
using PagedList;

namespace Forum.Web.Controllers
{
    [Authorize]
    public class SubscriptionController : CustomControllerBase
    {
        [HttpPost]
        public ActionResult AddSubscription(int topicId, int page)
        {
            var notificationController = new NotificationController();

            var client = new RestClient(Settings.Default.ForumApiUrl + "api/subscription/");
            var request = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };

            request.AddJsonBody(new Subscription()
            {
                AuthorId = GetIdByUserName(User.Identity.GetUserName()),
                TopicId = topicId,
            });
            var response = client.Execute<Subscription>(request);

            notificationController.SubscriptionNotification(response.Data);

            return new HttpStatusCodeResult(HttpStatusCode.Accepted);
        }

        public ActionResult DeleteSubscription(int topicId, int page, int subscriptionId)
        {
            var notificationController = new NotificationController();
            notificationController.DeleteSubscriptionNotification(subscriptionId);

            var client = new RestClient(Settings.Default.ForumApiUrl + "api/Subscription/{id}");
            var request = new RestRequest(Method.DELETE);

            request.AddParameter("id", subscriptionId);

            var response = client.Execute<Subscription>(request);

            return RedirectToAction("TopicList", "Topic", new { Page = page });
        }

        public List<Subscription> GetSubcriptionsByTopicId(int topicId)
        {
            var client = new RestClient(Settings.Default.ForumApiUrl);
            var request = new RestRequest("api/Subscription/GetSubscriptorsByTopicId/{topicId}", Method.GET);
            request.AddParameter("topicId", topicId);

            return client.Execute<List<Subscription>>(request).Data;
        }

        public Subscription GetSuscriptionByAuthorId(int authorId, int topicId)
        {
            var client = new RestClient(Settings.Default.ForumApiUrl);
            var request = new RestRequest("/api/Subscription/GetSuscriptionByAuthorId/{authorId}/{topicId}", Method.GET);
            request.AddParameter("authorId", authorId);
            request.AddParameter("topicId", topicId);

            var response = client.Execute<Subscription>(request);

            return response.Data;
        }
    }
}