
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using WebApi.Models;
using System.Web.Http;
using System;
using Forum.WebApi.Models;
namespace WebApi.Controllers
{
    public class PostController : ApiController
    {
        private static IList<Post> lst = new List<Post>()
        {
            new Post
            {
                Id = 123,
                Content = "Los dragones de Komodo son criaturas espectaculares",
                Topic = new Topic {
                   Id = 12345
                },
                UserName = "Franco Cunza"
            },
            new Post
            {
                Id = 543,
                Content = "Siii son bravazas",
                UserName = "Rossana Garcia Salirrosas"
            },
        };

        public IEnumerable<Post> Get()
        {
            return lst;
        }

        //public HttpResponseMessage GetById(int id)
        //{
        //    var post = lst.FirstOrDefault(x => x.Id == id);

        //    if (post == null) return Request.CreateResponse(HttpStatusCode.NotFound);

        //    return Request.CreateResponse<Post>(HttpStatusCode.Found, post); ;
        //}

        public HttpResponseMessage GetPostByTopicId(int topicId)
        {
            var post = lst.Where(x => x.Topic != null && x.Topic.Id == topicId).ToList();

            if (post == null) return Request.CreateResponse(HttpStatusCode.NotFound);

            return Request.CreateResponse<List<Post>>(HttpStatusCode.Found, post); ;
        }

        public HttpResponseMessage Post(Post post)
        {
            var newId = lst.Max(x => x.Id) + 1;
            post.Id = newId;
            lst.Add(post);

            var response = Request.CreateResponse<Post>(HttpStatusCode.Created, post);
            var uri = Url.Link("DefaultApi", new { id = post.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public HttpResponseMessage Put(int id, Post post)
        {
            var index = lst.ToList().FindIndex(x => x.Id == id);
            if (index >= 0)
            {
                post.Id = id;
                lst[index] = post;
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound);
        } 
	}
}