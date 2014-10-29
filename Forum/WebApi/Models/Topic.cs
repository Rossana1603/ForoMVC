using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.WebApi.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public DateTime CreateDate { get; set; }
        public List<String> Tags { get; set; }
    }
}