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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Topic>()
                .HasRequired(e => e.Author)
                .WithMany()
                .HasForeignKey(e => e.AuthorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Notification>()
                .HasRequired(e => e.Subscription)
                .WithMany()
                .HasForeignKey(e => e.SubscriptionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Notification>()
                .HasRequired(e => e.Post)
                .WithMany()
                .HasForeignKey(e => e.PostId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subscription>()
                .HasRequired(e => e.Author)
                .WithMany()
                .HasForeignKey(e => e.AuthorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Notification>()
                .HasRequired(e => e.Post)
                .WithMany()
                .HasForeignKey(e => e.PostId)
                .WillCascadeOnDelete(false);


        }

        public DbSet<Post> Post { get; set; }
        public DbSet<Topic> Topic { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<Subscription> Subscription { get; set; }
    }
}
