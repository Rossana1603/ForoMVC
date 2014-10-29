using Forum.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Post
    {
        public int Id { get; set; }
        public Topic Topic { get; set; }
        public List<String> Tags { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
    }
}