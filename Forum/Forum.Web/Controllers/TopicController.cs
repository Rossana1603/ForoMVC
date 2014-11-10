using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Forum.Web.Models;
using RestSharp;

namespace Forum.Web.Controllers
{
    public class TopicController : Controller
    {
        public ActionResult Index()
        {
            //var x = ConfigurationManager.AppSettings["ForumApiUrl"];
            var client = new RestClient("http://localhost:51713");
            var request = new RestRequest("api/topic/", Method.GET);
            var queryResult = client.Execute<List<TopicViewModel>>(request).Data;
            return View();
        }
	}
}