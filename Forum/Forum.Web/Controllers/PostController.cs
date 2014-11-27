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
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace Forum.Web.Controllers
{
    [Authorize]
    public class PostController :  CustomControllerBase
    {
        public PostController()
        {
            Mapper.CreateMap<Post,PostViewModel>()
                .ForMember(vwm=>vwm.Tags,opt=>opt.Ignore());
        }

        public ActionResult AddPost(int id)
        {            
            var model = new PostViewModel();
            model.TopicId = id;
            model.FromApiUrl = Settings.Default.ForumApiUrl;
            model.Author = new Author { Id = base.GetIdByUserName(User.Identity.GetUserName()) };    
            return View(model);
        }

        private List<PostViewModel> GetPostsByTopicId(int topicId)
        {
            var client = new RestClient(Settings.Default.ForumApiUrl);
            var request = new RestRequest("/api/Post/GetPostByTopicId/{topicId}", Method.GET);
            request.AddParameter("topicId", topicId);
            var response = client.Execute<List<Post>>(request);

            return Mapper.Map<List<Post>, List<PostViewModel>>(response.Data);
        }


        public ActionResult EditPost(int id, int postId)
        {
            var model = new PostViewModel();
            model = GetPostsByTopicId(id).FirstOrDefault(x => x.Id == postId);
            ViewBag.PostId = postId;
            return View(model);
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult AddPost(PostViewModel post)
        {
            var notificationController = new NotificationController();

            var client = new RestClient(Settings.Default.ForumApiUrl + "api/post/");
            var request = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };

            request.AddJsonBody(new Post()
            {
                TopicId = post.TopicId,
                Content = post.Content,
                Tags = post.Tags,
                AuthorId = base.GetIdByUserName(User.Identity.GetUserName()),
                UserName =  User.Identity.GetUserName()
            });
            var responsePost = client.Execute<Post>(request).Data;

            Task.Run(() => notificationController.ProcessNotifications(responsePost));

            return RedirectToAction("TopicDetail", "Topic", new { id = post.TopicId });
        }


        [HttpPost,ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(PostViewModel post)
        {
            var client = new RestClient(Settings.Default.ForumApiUrl + "api/Post/{id}");
            var content = post.Content;
            var request = new RestRequest(Method.PATCH) { RequestFormat = DataFormat.Json };

            post = GetPostsByTopicId(post.TopicId).FirstOrDefault(x => x.Id == int.Parse(Request["PostId"]));

            request.AddParameter("id", post.Id,ParameterType.UrlSegment);
            request.AddJsonBody(new Post()
            {
                TopicId = post.TopicId,
                Topic = post.Topic,
                Content = content,
                Author = post.Author,


                Tags = post.Tags,
                AuthorId = post.Author.Id,
                UserName = User.Identity.GetUserName(),
                Id = post.Id
            });
            var response = client.Execute<Post>(request);

            return RedirectToAction("TopicDetail", "Topic", new { id = post.TopicId });
        }

        public ActionResult DeletePost(int id, int postId)
        {
            var model = new PostViewModel();
            model = GetPostsByTopicId(id).FirstOrDefault(x => x.Id == postId);
            ViewBag.PostId = postId;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(PostViewModel post)
        {
            var client = new RestClient(Settings.Default.ForumApiUrl + "api/Post/{id}");
            var request = new RestRequest(Method.DELETE);
            var postId = int.Parse(Request["PostId"]);

            request.AddParameter("id", postId);

            var response = client.Execute<Post>(request);

            return RedirectToAction("TopicDetail", "Topic", new { id = post.TopicId });
        }
	}
}