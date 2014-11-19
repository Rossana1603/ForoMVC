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
    public class ControllerBase : Controller
    {
        protected int GetIdByUserName(string userName)
        {
            //http://localhost:51713/Api/Author/GetIdByUserName/?UserName=orlando1409%40gmail.com
            var client = new RestClient(Settings.Default.ForumApiUrl);
            var request = new RestRequest("/api/Author/GetIdByUserName/" + userName.Replace("@", "%40").Replace(".", "$"), Method.GET);

            var response = client.Execute<Author>(request);

            return response.Data.Id;
        }
    }
}