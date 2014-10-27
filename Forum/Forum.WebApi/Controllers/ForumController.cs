using Forum.WebApi.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace Forum.WebApi.Controllers
{
    public class ForumController : ApiController
    {
        private static IList<ForumModel> lst = new List<ForumModel>()
        {
            new ForumModel
            {
                ForumID = 12345,
                Titulo = "Dragones de Komodo",
                Contenido = "Los dragones de Komodo son criaturas espectaculares",
                FechaCreacion = System.DateTime.Now
            },
        };

        public IEnumerable<ForumModel> Get()
        {
            return lst;
        }
	}
}