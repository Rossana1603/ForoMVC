using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forum.Persistence.Domain;

namespace Forum.Persistence.Resolvers
{
    public static class DomainResolver
    {
        public static Post Clear(this Post post)
        {
            post.Content = string.Empty;
            post.Author = null;
            post.UserName = null;
            post.TopicId = 0;
            post.Topic = null;

            return post;
        }
    }
}
