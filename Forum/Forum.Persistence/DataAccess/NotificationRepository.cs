using Forum.Persistence.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Persistence.DataAccess
{
    public class NotificationRepository : Repository<Notification>
    {
        public NotificationRepository(DbContext context)
        {
            this._db = (ForumContext)context;
        }
    }
}
