using Forum.Persistence.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Web.Models
{
    public class PostViewModel
    {
        public int TopicId { get; set; }
        public virtual Topic Topic { get; set; }
        public List<String> Tags { get; set; }
        public string Content { get; set; }
        public virtual Author Author { get; set; }
        public int Id { get; set; }
    }
}