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

namespace Forum.Web.Controllers
{
    [Authorize]
    public class TopicController : CustomControllerBase
    {
        public TopicController()
        {
            Mapper.CreateMap<Topic,TopicViewModel>()
                .ForMember(vwm=>vwm.Tags,opt=>opt.Ignore());

            Mapper.CreateMap<Post, PostViewModel>()
                .ForMember(vwm => vwm.Tags, opt => opt.Ignore());
        }
        public ActionResult TopicList(int? page)
        {
            ViewBag.Error = false;
            int pageNumber = page ?? 1;
            int pageSize = 2;

            var client = new RestClient(Settings.Default.ForumApiUrl);
            var request = new RestRequest("api/topic/", Method.GET);
            var response = client.Execute<List<Topic>>(request);
            var subscriptionController = new SubscriptionController();

            if (response.StatusCode != HttpStatusCode.OK)
            {
                ViewBag.Error = true;
                return View();
            }

            var topics = Mapper.Map<List<Topic>,List<TopicViewModel>>(response.Data);
            var currentAuthorId = GetIdByUserName(User.Identity.GetUserName());            
            topics.ForEach( (x) =>
                            {
                                x.AvatarFileName = base.GetAvatarFileName(GetIdByUserName(x.Author.UserName));
                                x.Subscription = subscriptionController.GetSuscriptionByAuthorId(currentAuthorId, x.Id);
                            });

            return View(topics.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult TopicDetail(int id, int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 2;
            var totalItemCount = 0;

            var posts = new PostController().GetPostsByTopicId(id, pageNumber, pageSize, out totalItemCount);                   

            posts.ToList().ForEach((x) =>
            {
                x.IsTopicCreator = x.Author.UserName == User.Identity.GetUserName();
                x.AvatarFileName = base.GetAvatarFileName(x.Author.Id);
            });

            var topic = Mapper.Map<Topic, TopicViewModel>(posts != null ? posts.First().Topic : new TopicController().GetTopic(id));            
            var postsPage = new StaticPagedList<PostViewModel>(posts.ToList(), pageNumber, pageSize, totalItemCount);

            return View(new TopicDetailViewModel {
                            Topic = topic,
                            Posts = postsPage
            });
        }

        public ActionResult AddTopic()
        {
            var model = new TopicViewModel();
            return View(model);
        }
        
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult AddTopic(TopicViewModel topic)
        {
            var client = new RestClient(Settings.Default.ForumApiUrl+"api/topic/");
            var request = new RestRequest(Method.POST) {RequestFormat = DataFormat.Json};

            request.AddJsonBody(new Topic()
            {
                Title = topic.Title,
                Content = topic.Content,
                CreateDate = DateTime.Now,
                UserName = User.Identity.GetUserName(),
                AuthorId = GetIdByUserName(User.Identity.GetUserName())
            });
            var response = client.Execute<Topic>(request);
            return RedirectToAction("TopicList");
        }
            
        public ActionResult DeleteTopic(int id)
        {
            var client = new RestClient(Settings.Default.ForumApiUrl);
            var request = new RestRequest("api/topic/{id}", Method.DELETE);
            request.AddParameter("id", id);
            var queryResult = client.Execute<List<TopicViewModel>>(request).Data;
            return RedirectToAction("Index");
        }

        public Topic GetTopic(int id)
        {
            var client = new RestClient(Settings.Default.ForumApiUrl);
            var request = new RestRequest("api/topic/{id}", Method.GET) { RequestFormat = DataFormat.Json };
            request.AddParameter("id", id);
            return client.Execute<Topic>(request).Data;
        }

    }
}