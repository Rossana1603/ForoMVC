using Forum.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq;

namespace Forum.WebApi.Controllers
{
    public class TopicController : ApiController
    {
        private static IList<Topic> lst = new List<Topic>()
        {
            new Topic
            {
                Id = 12345,
                Title = "Dragones de Komodo",
                Content = "Los dragones de Komodo son criaturas espectaculares",
                CreateDate = System.DateTime.Now,
                Tags = new List<string>{ "#Dragones", "#Espectacular", "#Criaturas"}
            },
            new Topic
            {
                Id = 98765,
                Title = "Las Artes Oscuras",
                Content = "Las clases del profesor Snape son muy interesantes.",
                CreateDate = System.DateTime.Now
            },
        };

        public IEnumerable<Topic> Get()
        {
            return lst;
        }

        public HttpResponseMessage Get(int id)
        {
            var topic = lst.FirstOrDefault(x => x.Id == id);

            if(topic == null) return Request.CreateResponse(HttpStatusCode.NotFound);

            return Request.CreateResponse<Topic>(HttpStatusCode.Found, topic);
        }

        public HttpResponseMessage GetTagsByTopicId(int id)
        {
            var tags = lst.FirstOrDefault(x => x.Id == id);

            if (tags == null) return Request.CreateResponse(HttpStatusCode.NotFound);

            return Request.CreateResponse<List<String>>(HttpStatusCode.Found, tags.Tags); 
        }

        public HttpResponseMessage Post(Topic topic)
        {
            var newId = lst.Max(x => x.Id) + 1;
            topic.Id = newId;
            lst.Add(topic);

            var response = Request.CreateResponse<Topic>(HttpStatusCode.Created, topic);
            var uri = Url.Link("DefaultApi", new { id = topic.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public HttpResponseMessage Put(int id, Topic topic)
        {
            var index = lst.ToList().FindIndex(x => x.Id == id);
            if (index >= 0)
            {
                topic.Id = id;
                lst[index] = topic;
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound);
        } 
	}
}