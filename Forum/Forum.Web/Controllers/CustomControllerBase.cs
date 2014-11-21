using Forum.Persistence.Domain;
using Forum.Web.Properties;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Security;


namespace Forum.Web.Controllers
{
    public class CustomControllerBase : Controller
    {
        protected int GetIdByUserName(string userName)
        {
            //http://localhost:51713/Api/Author/GetIdByUserName/?UserName=orlando1409%40gmail.com
            var client = new RestClient(Settings.Default.ForumApiUrl);
            var request = new RestRequest("/api/Author/GetIdByUserName/" + userName.Replace("@", "%40").Replace(".", "$"), Method.GET);

            var response = client.Execute<Author>(request);

            return response.Data.Id;
        }

        protected string GetAvatarFileName(string userName)
        {
            var id = GetIdByUserName(userName);
            var filePaths = Directory.GetFiles(Server.MapPath("~/Content/Images")).ToList();                            
            var onePath = filePaths.FirstOrDefault(x => Path.GetFileNameWithoutExtension(x) == id.ToString());
            return Path.GetFileName(onePath) ?? string.Empty;
        }

        protected void RegisteredUserAuthor(string email)
        {
            var client = new RestClient(Settings.Default.ForumApiUrl + "api/author/");
            var request = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };

            request.AddJsonBody(new Author()
            {
                Email = email,
                UserName = email
            });

            var response = client.Execute<Author>(request);               
        }
    }
}