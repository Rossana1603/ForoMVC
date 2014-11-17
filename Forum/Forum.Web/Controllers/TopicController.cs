using System;
using System.Collections.Generic;
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
    public class TopicController : Controller
    {
        public TopicController()
        {
            Mapper.CreateMap<Topic,TopicViewModel>()
                .ForMember(vwm=>vwm.Tags,opt=>opt.Ignore());
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

        private static int GetIdByUserName(string userName)
        {
            //http://localhost:51713/Api/Author/GetIdByUserName/?UserName=orlando1409%40gmail.com
            var client = new RestClient(Settings.Default.ForumApiUrl);
            var request = new RestRequest("/api/Author/GetIdByUserName/" + userName.Replace("@", "%40").Replace(".", "$"), Method.GET);

            var response = client.Execute<Author>(request);

            return response.Data.Id;
        }
            
        public ActionResult DeleteTopic(int id)
        {
            var client = new RestClient(Settings.Default.ForumApiUrl);
            var request = new RestRequest("api/topic/{id}", Method.DELETE);
            request.AddParameter("id", id);
            var queryResult = client.Execute<List<TopicViewModel>>(request).Data;
            return RedirectToAction("Index");
        }

        public ActionResult GeTopicById(int id)
        {
            var client = new RestClient(Settings.Default.ForumApiUrl);
            var request = new RestRequest("/api/topic/{id}", Method.GET);
            request.AddParameter("id", id);
            var response = client.Execute<Topic>(request);
            var result = response.Data.Id;
            return RedirectToAction("TopicList");
        }
	}
}