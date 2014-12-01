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
                                x.Subscription = GetSuscriptionByAuthorId(currentAuthorId, x.Id);
                            });

            return View(topics.ToPagedList(pageNumber, pageSize));
        }

        //public ActionResult TopicDetail(int id, int? page, string jsonTopic)
        public ActionResult TopicDetail(int id, int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 2;

            var client = new RestClient(Settings.Default.ForumApiUrl);
            var request = new RestRequest("/api/Post/GetPostByTopicId/{topicId}", Method.GET) { RequestFormat = DataFormat.Json};
            request.AddParameter("topicId", id);
            request.AddParameter("pageNumber", pageNumber);
            request.AddParameter("pageSize", pageSize);
            var response = client.Execute<List<Post>>(request);

            var requestTopic = new RestRequest("/api/topic/{id}", Method.GET);
            requestTopic.AddParameter("id", id);
            var responseTopic = client.Execute<Topic>(requestTopic);

            var topic = Mapper.Map<Topic, TopicViewModel>(response.Data != null && response.Data.Count>0 ? response.Data.FirstOrDefault(x=>x.Topic!=null).Topic : responseTopic.Data);            
            //var posts = Mapper.Map<List<Post>, List<PostViewModel>>(response.Data);

            var totalItemCount = Convert.ToInt32(response.Headers.FirstOrDefault(x => x.Name == "X-TotalItemCount").Value);            

            posts.ToList().ForEach((x) =>
            {
                x.IsTopicCreator = x.Author.UserName == User.Identity.GetUserName();
                x.AvatarFileName = base.GetAvatarFileName(x.Author.Id);
            });
            
            return View(new TopicDetailViewModel { Topic = topic, Posts = new StaticPagedList<PostViewModel>(posts.ToList(),pageNumber, pageSize, totalItemCount) });
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

        private Subscription GetSuscriptionByAuthorId(int authorId, int topicId)
        {
            var client = new RestClient(Settings.Default.ForumApiUrl);
            var request = new RestRequest("/api/Subscription/GetSuscriptionByAuthorId/{authorId}/{topicId}", Method.GET);
            request.AddParameter("authorId",authorId);
            request.AddParameter("topicId",topicId);
            
            var response = client.Execute<Subscription>(request);

            return response.Data;
        }
    }
}