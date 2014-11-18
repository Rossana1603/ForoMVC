using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Forum.Persistence.Domain;
using Forum.Web.Models;
using Forum.Web.Properties;
using RestSharp;

namespace Forum.Web.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        public PostController()
        {
            Mapper.CreateMap<Post,PostViewModel>()
                .ForMember(vwm=>vwm.Tags,opt=>opt.Ignore());
        }

        public ActionResult AddPost()
        {
            var model = new PostViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPost(PostViewModel post)
        {
            var client = new RestClient(Settings.Default.ForumApiUrl + "api/post/");
            var request = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };

            request.AddJsonBody(new Post()
            {
                TopicId = post.TopicId,
                Content = post.Content,
                Author = post.Author,
                Tags =  post.Tags
            });
            var response = client.Execute<Post>(request);
            return RedirectToAction("TopicList");
        }

        public ActionResult DeletePost(int id)
        {
            var client = new RestClient(Settings.Default.ForumApiUrl);
            var request = new RestRequest("api/post/{id}", Method.DELETE);
            request.AddParameter("id", id);
            var queryResult = client.Execute<List<PostViewModel>>(request).Data;
            return RedirectToAction("TopicList");
        }
	}
}