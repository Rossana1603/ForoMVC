using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Forum.Persistence.Domain;
using Forum.Persistence.DataAccess;

namespace WebApi.Controllers
{
    public class AuthorController : EntityControllerBase<Author>
    {
        public AuthorController(IRepository<Author> repository)
            : base(repository)
        {
            
        }
	}
}