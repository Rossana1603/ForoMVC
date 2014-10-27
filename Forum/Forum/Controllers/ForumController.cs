using Forum.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Forum.Controllers
{
    public class ForumController : Controller
    {
        private HttpClient client;

        private HttpClient GetHttpClient()
        {
            client = new HttpClient();
            
            // New code:
            client.BaseAddress = new Uri("http://localhost:51713/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            return client;
        }


        private async Task<IEnumerable<ForumModel>> Test()
        {
            IEnumerable<ForumModel> lstForums = new List<ForumModel>();
            using (var client = GetHttpClient())
            {
                // New code:
                HttpResponseMessage response = await client.GetAsync("api/forum");
                if (response.IsSuccessStatusCode)
                {
                    lstForums = await response.Content.ReadAsAsync<IEnumerable<ForumModel>>();
                }
            }

            return lstForums;
        }

        //
        // GET: /Forum/
        public ActionResult Index()
        {
            var test = Test();
            return View();
        }
	}
}