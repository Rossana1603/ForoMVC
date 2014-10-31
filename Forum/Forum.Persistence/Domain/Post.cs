using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Persistence.Domain
{
    public class Post
    {
        public int Id { get; set; }
        public Topic Topic { get; set; }
        public List<String> Tags { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
        public Author Author { get; set; }
    }
}
