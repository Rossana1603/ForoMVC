using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.WebApi.Models
{
    public class ForumModel
    {
        public int ForumID { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}