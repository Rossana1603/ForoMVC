using Forum.Persistence.DataAccess;
using Forum.Persistence.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class NotificationController : EntityControllerBase<Notification>
    {
        public NotificationController(IRepository<Notification> repository): base(repository)
        {
                    
        }

        [Route("api/Notification/DeleteByPostId/{postId}")]
        public HttpResponseMessage DeleteByPostId([FromUri]int postId)
        {
            List<Notification> list = base.Get().Cast<Notification>().ToList();
            var entity = list.FirstOrDefault(x => x.PostId == postId);
            var response = base.Delete(entity.Id).IsSuccessStatusCode;
            return (!response) ? Request.CreateResponse(HttpStatusCode.NotFound) : Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}