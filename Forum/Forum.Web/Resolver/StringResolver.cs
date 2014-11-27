using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Web.Resolver
{
    public static class StringResolver
    {
        public static string ToContentPreview(this string content, string userName)
        {
            var limit = 30;
            var substring = content.Length <= limit ? content : content.Substring(0, limit);
            return userName + " " + "posted this: " + substring + "...";
        }
    }
}