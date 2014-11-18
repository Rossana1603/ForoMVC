using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Forum.Persistence.DataAccess;
using System.Web.Http;
using System;
using Forum.Persistence.Domain;
using System.Web.Http.OData;
namespace WebApi.Controllers
{


    public class PostController : EntityControllerBase<Post>
    {
        public PostController(IRepository<Post> repository) :base (repository)
        {
            
        }
        
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Mvc.HttpGet]
        [Route("api/Post/GePostByTopicId/{topicId}")]
        public HttpResponseMessage GePostByTopicId([FromUri]int topicId)
        {
            List<Post> list = Get().ToList();
            var entity = list.Where(x => x.TopicId == topicId).ToList();

            return (!entity.Any()) ? Request.CreateResponse(HttpStatusCode.NotFound) : Request.CreateResponse<List<Post>>(HttpStatusCode.Found, entity);
        }
    }
}