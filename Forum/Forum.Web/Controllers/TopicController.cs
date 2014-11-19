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

namespace Forum.Web.Controllers
{
    [Authorize]
    public class TopicController : ControllerBase
    {
        public TopicController()
        {
            Mapper.CreateMap<Topic,TopicViewModel>()
                .ForMember(vwm=>vwm.Tags,opt=>opt.Ignore());

            Mapper.CreateMap<Post, PostViewModel>()
                .ForMember(vwm => vwm.Tags, opt => opt.Ignore());

        }
        public ActionResult TopicList()
        {
            var client = new RestClient(Settings.Default.ForumApiUrl);
            var request = new RestRequest("api/topic/", Method.GET);
            var response = client.Execute<List<Topic>>(request);
            ViewBag.Error = false;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                //TODO mostrar un mensaje de error
                ViewBag.Error = true;
                return View();
            }
            return View(Mapper.Map<List<Topic>,List<TopicViewModel>>(response.Data));
        }


        public ActionResult TopicDetail(int id)
        {
            var client = new RestClient(Settings.Default.ForumApiUrl);
            var request = new RestRequest("/api/Post/GetPostByTopicId/{topicId}", Method.GET);
            request.AddParameter("topicId", id);
            var response = client.Execute<List<Post>>(request);

            var requestTopic = new RestRequest("/api/topic/{id}", Method.GET);
            requestTopic.AddParameter("id", id);
            var responseTopic = client.Execute<Topic>(requestTopic);

            var topicDetailModel = new TopicDetailViewModel();
            topicDetailModel.Topic = Mapper.Map<Topic, TopicViewModel>(response.Data != null && response.Data.Count>0 ? response.Data.FirstOrDefault().Topic : responseTopic.Data);
            topicDetailModel.Posts = Mapper.Map<List<Post>, List<PostViewModel>>(response.Data);
            return View(topicDetailModel);
        }


        public ActionResult AddTopic()
        {
            var model = new TopicViewModel();
            return View(model);
        }
        
        [HttpPost]
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

	}
}