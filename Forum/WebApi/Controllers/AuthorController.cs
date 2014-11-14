using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

        [HttpGet]
        public HttpResponseMessage GetIdByUserName(string userName)
        {
            var list = base.Get();
            var entity = list.FirstOrDefault();

            return (entity == null) ? Request.CreateResponse(HttpStatusCode.NotFound) : Request.CreateResponse<Author>(HttpStatusCode.Found, entity);
        }

	}
}