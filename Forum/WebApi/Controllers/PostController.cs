using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Forum.Persistence.DataAccess;
using System.Web.Http;
using System;
using Forum.Persistence.Domain;
using System.Web.Http.OData;
namespace WebApi.Controllers
{


    public class PostController : EntityControllerBase<Post>
    {
        public PostController(IRepository<Post> repository) :base (repository)
        {
            
        }
    }
}