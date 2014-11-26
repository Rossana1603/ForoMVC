using Forum.Persistence.DataAccess;
using Forum.Persistence.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApi.Controllers
{
    public class NotificationController : EntityControllerBase<Notification>
    {
        public NotificationController(IRepository<Notification> repository): base(repository)
        {
                    
        }
    }
}