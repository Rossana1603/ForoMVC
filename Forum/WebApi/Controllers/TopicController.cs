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

        [Route("api/Topic/GetTopicsByKeywords/{keywords}")]
        public HttpResponseMessage GetTopicsByKeywords(string keywords)
        {
            HttpResponseMessage response = null;
            var splitKeyWords = keywords==null ? new string[]{null} : keywords.Split();

            var result = Query.Where( x =>
                            splitKeyWords.Any(key => x.Title.Contains(key??x.Title))
                         || splitKeyWords.Any(key => x.Content.Contains(key??x.Content))
                         ).ToList();

            if (result != null)
            {
                response = Request.CreateResponse<List<Topic>>(HttpStatusCode.Found, result);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return response;
        }
    }
}