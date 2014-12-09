using Forum.Persistence.DataAccess;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;
using Forum.Persistence.Domain;
using System.Linq;

namespace WebApi.Controllers
{
    public class TopicController : EntityControllerBase<Topic>
    {
        public TopicController(IRepository<Topic> repository) : base(repository)
        {           
        }

        //[Route("api/Topic/SearchTopics/{keywords}/{pageNumber}/{pageSize}")]
        //public HttpResponseMessage SearchTopics([FromUri]string keywords)
        //{
        //    HttpResponseMessage response = null;
        //    var totalItemCount = 0;
        //    var posts = new List<Post>();
        //    //var query = Query.Where(x => x.TopicId == topicId);

        //    var result = Query.Where(x => x.Content.Any(i => keywords.Any(kw => kw == i.Name))
        //            || x.Title.Any(d => keywords.Any(k => x.Name == k))
        //            || keywords.Any(kew => x.Name.Contains(kew)));

        //    return result;

        //    var page = query
        //        .OrderBy(x => x.Id)
        //        .Select(x => x)
        //        .Where(x => x.TopicId == topicId)
        //        .Skip(pageSize * (--pageNumber))
        //        .Take(pageSize)
        //        .GroupBy(x => new { TotalItemCount = query.Count() })
        //        .FirstOrDefault();

        //    if (page != null)
        //    {
        //        totalItemCount = page.Key.TotalItemCount;
        //        posts = page.Select(x => x).ToList();
        //    }

        //    if (posts.Any())
        //    {
        //        response = Request.CreateResponse<List<Post>>(HttpStatusCode.Found, posts);
        //        response.Headers.Add("X-TotalItemCount", totalItemCount.ToString());
        //    }
        //    else
        //    {
        //        response = Request.CreateResponse(HttpStatusCode.NotFound);
        //    }

        //    return response;
        //}
    }
}