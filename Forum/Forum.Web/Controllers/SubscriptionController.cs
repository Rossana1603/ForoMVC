using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Forum.Persistence.Domain;
using Forum.Web.Models;
using Forum.Web.Properties;
using Microsoft.AspNet.Identity;
using RestSharp;
using PagedList;

namespace Forum.Web.Controllers
{
    public class SubscriptionController : CustomControllerBase
    {

        public ActionResult AddSubscription(int topicId, int page)
        {
            var model = new TopicViewModel();

            var client = new RestClient(Settings.Default.ForumApiUrl + "api/subscription/");
            var request = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };

            request.AddJsonBody(new Subscription()
            {
                AuthorId = GetIdByUserName(User.Identity.GetUserName()),
                TopicId  = topicId,                
            });
            var response = client.Execute<Topic>(request);

            return RedirectToAction("TopicList", "Topic", new { Page = page });
        }

        public ActionResult DeleteSubscription(int topicId, int page, int subscriptionId)
        {
            var model = new TopicViewModel();

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
    }
}