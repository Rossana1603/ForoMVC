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
    public class AuthorController : EntityControllerBase<Author>
    {
        public AuthorController(IRepository<Author> repository)
            : base(repository)
        {
            
        }

        [Route("api/Author/GetIdByUserName/{userName}")]
        public HttpResponseMessage GetIdByUserName([FromUri]string userName)
        {
            List<Author> list = base.Get().Cast<Author>().ToList();
            var entity = list.FirstOrDefault(x=>x.UserName == userName.Replace("$","."));

            return (entity == null) ? Request.CreateResponse(HttpStatusCode.NotFound) : Request.CreateResponse<Author>(HttpStatusCode.Found, entity);
        }

	}
}