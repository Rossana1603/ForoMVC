using Forum.Persistence.DataAccess;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;
using Forum.Persistence.Domain;

namespace WebApi.Controllers
{
    public class TopicController : EntityControllerBase<Topic>
    {
        public TopicController(IRepository<Topic> repository) : base(repository)
        {           
        }
    }
}