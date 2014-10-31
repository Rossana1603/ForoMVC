using System.Data.Entity;
using Forum.Persistence.Domain;

namespace Forum.Persistence.DataAccess
{
    public class ForumContext : DbContext
    {
        public ForumContext()
            : base(nameOrConnectionString: "Forum")
        {

        }

        public DbSet<Post> Post { get; set; }
        public DbSet<Topic> Topic { get; set; }
    }
}
