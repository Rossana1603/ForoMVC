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
        //
        // GET: /Forum/
        public ActionResult Index()
        {
            return View();
        }
	}
}