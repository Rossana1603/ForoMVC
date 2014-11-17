using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Forum.Persistence.Domain;
using Forum.Web.Properties;
using RestSharp;

namespace Forum.Web.Controllers
{
    [Authorize]
    public class PostController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GePostByTopicId(int id)
        {
            var client = new RestClient(Settings.Default.ForumApiUrl);
            var request = new RestRequest("/api/post/GePostByTopicId/{id}", Method.GET);
            request.AddParameter("id", id);
            var response = client.Execute<List<Post>>(request);
            var result = response.Data;
            return View();
        }
	}
}