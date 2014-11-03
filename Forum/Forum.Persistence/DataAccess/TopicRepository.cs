using Forum.Persistence.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
