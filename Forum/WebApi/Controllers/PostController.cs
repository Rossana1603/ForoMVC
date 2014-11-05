
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Forum.Persistence.DataAccess;
using System.Web.Http;
using System;
using Forum.Persistence.Domain;
using System.Web.Http.OData;
namespace WebApi.Controllers
{
    public class PostController : ApiController
    {

        private readonly PostRepository _postRepository = new PostRepository(new ForumContext());

        public IEnumerable<Post> Get()
        {
            return _postRepository.Query();
        }

        public HttpResponseMessage GetById(int id)
        {
            var post = _postRepository.Get(id);

            return (post == null) ? Request.CreateResponse(HttpStatusCode.NotFound) : Request.CreateResponse<Post>(HttpStatusCode.Found, post);
        }

        public HttpResponseMessage Post(Post post)
        {
            _postRepository.Add(post);
            var response = Request.CreateResponse<Post>(HttpStatusCode.Created, post);
            var uri = Url.Link("DefaultApi", new { id = post.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public HttpResponseMessage Put(int id, Post post)
        {
            var response = _postRepository.Update(post);
            return (!response) ? Request.CreateResponse(HttpStatusCode.NotFound) : Request.CreateResponse(HttpStatusCode.NoContent);
        }

        public HttpResponseMessage Patch(int id, Delta<Post> deltaTopic)
        {
            var post = _postRepository.Get(id);
            deltaTopic.Patch(post);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
	}
}