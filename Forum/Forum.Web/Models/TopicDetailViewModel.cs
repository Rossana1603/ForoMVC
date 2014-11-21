using System.Collections;
using System.Collections.Generic;

namespace Forum.Web.Models
{
    public class TopicDetailViewModel : ModelBase
    {
        public TopicViewModel Topic { get; set; }
        public IList<PostViewModel> Posts { get; set; }
    }
}