using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Forum.Persistence.Domain;
using Forum.Persistence.DataAccess;
using System.Net.Http;
using System.Net;

namespace WebApi.Controllers
{
    public class SubscriptionController : EntityControllerBase<Subscription>
    {
        public SubscriptionController(IRepository<Subscription> repository): base(repository)
        {
                    
        }
	}
}