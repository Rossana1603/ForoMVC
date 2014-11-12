using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Forum.Persistence.Domain;
using Forum.Web.Models;
using Forum.Web.Properties;
using RestSharp;

namespace Forum.Web.Controllers
{
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
            var topics = client.Execute<List<Topic>>(request).Data;

            return View(Mapper.Map<List<Topic>,List<TopicViewModel>>(topics));
        }


        public ActionResult AddTopic(TopicViewModel topic)
        {
            var client = new RestClient(Settings.Default.ForumApiUrl);
            var request = new RestRequest("api/topic/", Method.POST);
            request.AddBody(new Topic
            {
                Title = topic.Title,
                Content = topic.Content,
                CreateDate = topic.CreateDate,
                Tags = topic.Tags
            });
            var queryResult = client.Execute<List<TopicViewModel>>(request).Data;
            return RedirectToAction("Index") ;
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