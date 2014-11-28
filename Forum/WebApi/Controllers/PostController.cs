using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Forum.Persistence.DataAccess;
using System.Web.Http;
using System;
using Forum.Persistence.Domain;
using System.Web.Http.OData;
using PagedList;
using Forum.Persistence.Resolvers;

namespace WebApi.Controllers
{


    public class PostController : EntityControllerBase<Post>
    {
        public PostController(IRepository<Post> repository) : base(repository)
        {

        }

        [Route("api/Post/GetPostByTopicId/{topicId}")]
        public HttpResponseMessage GetPostByTopicId([FromUri]int topicId,[FromUri]int pageNumber,[FromUri]int pageSize)
        {
            var posts = Get()
                        .Where(x => x.TopicId == topicId)
                        .ToList();

            var pagedList = posts.ToPagedList(pageNumber, pageSize);

            posts.ForEach((x) =>{
                    if (!pagedList.Any(y => y.Id == x.Id))
                    {
                        x = x.Clear();
                    }
            });

            return (!posts.Any()) ? 
                    Request.CreateResponse(HttpStatusCode.NotFound) : 
                    Request.CreateResponse<List<Post>>(HttpStatusCode.Found, posts);
        }
    }
}