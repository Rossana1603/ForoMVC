using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Web.Models
{
    public class TopicViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public List<String> Tags { get; set; }
    }
}