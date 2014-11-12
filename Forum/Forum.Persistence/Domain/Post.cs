using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Persistence.Domain
{
    public class Post : Entidad
    {
        public int TopicId { get; set; }
        public virtual Topic Topic { get; set; }
        public List<String> Tags { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
        public virtual Author Author { get; set; }
        public int AuthorId { get; set; }
    }
}
