using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forum.Web.Models
{
    public class CustomModelBase
    {
        public string AvatarFileName { get; set; }
        public int PageCount { get; set; }
        public int PageNumber { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
    }
}
