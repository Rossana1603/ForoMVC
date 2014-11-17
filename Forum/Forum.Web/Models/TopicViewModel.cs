using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Forum.Persistence.Domain;

namespace Forum.Web.Models
{
    public class TopicViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public List<String> Tags { get; set; }
        public Author Author { get; set; }
        public string UserName { get; set; }
    }
}