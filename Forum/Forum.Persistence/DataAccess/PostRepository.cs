using Forum.Persistence.Domain;
using System.Data.Entity;

namespace Forum.Persistence.DataAccess
{
    public class PostRepository : Repository<Post>
    {
        public PostRepository(DbContext context)
        {
            this._db = (ForumContext)context;
        }
    }
}
