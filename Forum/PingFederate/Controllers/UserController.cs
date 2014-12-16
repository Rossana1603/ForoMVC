using PingFederate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;

namespace PingFederate.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        // GET: User
        public ActionResult LoginUser(User user)
        {
            PrincipalContext pc = null;
            var primaryServer = "mutare.com";
            var secondaryServer = "OCASTILLO";
            var userName = user.UserName;
            var password = user.Password;
            var result = false;
            try
            {
                pc = new PrincipalContext(ContextType.Domain, primaryServer);
                result = pc.ValidateCredentials(userName, password);
                if (result) return View("Ok");
            }
            catch (Exception)
            {
                if (!string.IsNullOrEmpty(secondaryServer))
                {
                    //pc = new PrincipalContext(ContextType.Domain, secondaryServer);
                    //result = pc.ValidateCredentials(userName, password);

                    pc = new PrincipalContext(ContextType.Machine);
                    result = pc.ValidateCredentials(userName, password);
                    if (result)  return View("OkMachine");
                }
            }
            finally
            {
                if (pc != null) pc.Dispose();
            }

            return View("Index");
        }
    }
}