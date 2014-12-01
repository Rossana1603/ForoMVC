using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Forum.Persistence.DataAccess;
using System.Web.Http;
using Forum.Persistence.Domain;
using PagedList;
using System.Web;
using Newtonsoft.Json;
using System.Net.Http.Formatting;

namespace WebApi.Controllers
{


    public class PostController : EntityControllerBase<Post>
    {
        private List<Post> responsePosts;

        public PostController(IRepository<Post> repository) : base(repository)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="topicId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>
        /// - A list of Topics
        /// - Headers: --using naming convention as stated in RFC 2047--
        ///     X-TotalItemCount: Total amount of items 
        /// </returns>
        [Route("api/Post/GetPostsByTopicId/{topicId}/{pageNumber}/{pageSize}")]
        public HttpResponseMessage GetPostsByTopicId([FromUri]int topicId, [FromUri]int pageNumber, [FromUri]int pageSize)
        {
            HttpResponseMessage response = null;
            var totalItemCount = 0;
            var posts = new List<Post>();
            var query = Query.Where(x => x.TopicId == topicId);

            var page = query
                .OrderBy(x=>x.Id)
                .Select(x => x)
                .Where(x => x.TopicId == topicId)
                .Skip(pageSize * pageNumber)
                .Take(pageSize)
                .GroupBy(x => new { TotalItemCount = query.Count() })
                .FirstOrDefault();

            if (page!=null)
            {            
                totalItemCount = page.Key.TotalItemCount;
                posts = page.Select(x => x).ToList();            
            }

            if (posts.Any())
            {
                response = Request.CreateResponse<List<Post>>(HttpStatusCode.Found, posts);
                response.Headers.Add("X-TotalItemCount", totalItemCount.ToString());
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return response;
        }
    }
}