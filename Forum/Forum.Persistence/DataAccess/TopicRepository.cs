using Forum.Persistence.Domain;
using System.Data.Entity;

namespace Forum.Persistence.DataAccess
{
    public class TopicRepository : Repository<Topic>
    {
        public TopicRepository(DbContext context)
        {
            this._db = (ForumContext)context;
        }
    }
}
