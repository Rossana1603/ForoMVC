using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forum.Persistence.Domain;

namespace Forum.Persistence.DataAccess
{
    public class AuthorRepository : Repository<Author>
    {
        public AuthorRepository(DbContext context)
        {
            this._db = (ForumContext)context;
        }

    }
}
