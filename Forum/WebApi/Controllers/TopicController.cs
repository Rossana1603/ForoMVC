using Forum.Persistence.DataAccess;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;
using System.Linq;
using Forum.Persistence.Domain;

namespace WebApi.Controllers
{
    public class TopicController : ApiController
    {
        private readonly TopicRepository _topicRepository = new TopicRepository(new ForumContext());

        public IEnumerable<Topic> Get()
        {
            return _topicRepository.Query();
        }

        public HttpResponseMessage Get(int id)
        {
            var topic = _topicRepository.Get(id);

            return (topic == null) ? Request.CreateResponse(HttpStatusCode.NotFound) : Request.CreateResponse<Topic>(HttpStatusCode.Found, topic);
        }

        public HttpResponseMessage Post(Topic topic)
        {
            _topicRepository.Add(topic);
            var response = Request.CreateResponse<Topic>(HttpStatusCode.Created, topic);
            var uri = Url.Link("DefaultApi", new { id = topic.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public HttpResponseMessage Put(int id, Topic topic)
        {
            var response = _topicRepository.Update(topic);
            return (!response) ? Request.CreateResponse(HttpStatusCode.NotFound) : Request.CreateResponse(HttpStatusCode.NoContent);
        }

        public HttpResponseMessage Patch(int id, Delta<Topic> deltaTopic)
        {
            var topic = _topicRepository.Get(id);
            deltaTopic.Patch(topic);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
	}
}