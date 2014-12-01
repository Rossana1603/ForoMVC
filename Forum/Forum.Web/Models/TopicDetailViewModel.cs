using PagedList;
using System.Collections;
using System.Collections.Generic;

namespace Forum.Web.Models
{
    public class TopicDetailViewModel : CustomModelBase
    {
        public TopicViewModel Topic { get; set; }
        //public IPagedList<PostViewModel> Posts { get; set; }
        public StaticPagedList<PostViewModel> Posts { get; set; }
    }
}