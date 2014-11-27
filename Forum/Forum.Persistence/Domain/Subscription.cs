using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Persistence.Domain
{
    public class Subscription : Entidad
    {
        public int TopicId { get; set; }
        public virtual Topic Topic { get; set; }
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public string Message { get; set; }

    }
}
