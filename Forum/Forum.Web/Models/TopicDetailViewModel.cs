using System.Collections;
using System.Collections.Generic;

namespace Forum.Web.Models
{
    public class TopicDetailViewModel : CustomModelBase
    {
        public TopicViewModel Topic { get; set; }
        public IList<PostViewModel> Posts { get; set; }
    }
}