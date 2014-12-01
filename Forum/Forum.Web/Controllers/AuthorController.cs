using Forum.Persistence.Domain;
using Forum.Web.Properties;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.Web.Controllers
{
    [Authorize]
    public class AuthorController : CustomControllerBase
    {
        public Author GetAuthor(int id)
        {
            var client = new RestClient(Settings.Default.ForumApiUrl);
            var request = new RestRequest("api/author/{id}", Method.GET) { RequestFormat = DataFormat.Json };
            request.AddParameter("id", id);
            return client.Execute<Author>(request).Data;
        }
	}
}