namespace Forum.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubscriptionId = c.Int(nullable: false),
                        NotificationDate = c.DateTime(nullable: false),
                        State = c.Int(nullable: false),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostId)
                .ForeignKey("dbo.Subscriptions", t => t.SubscriptionId)
                .Index(t => t.SubscriptionId)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TopicId = c.Int(nullable: false),
                        UserName = c.String(),
                        Content = c.String(),
                        AuthorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Authors", t => t.AuthorId, cascadeDelete: true)
                .ForeignKey("dbo.Topics", t => t.TopicId, cascadeDelete: true)
                .Index(t => t.TopicId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Content = c.String(nullable: false),
                        UserName = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        AuthorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Authors", t => t.AuthorId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TopicId = c.Int(nullable: false),
                        AuthorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Authors", t => t.AuthorId)
                .ForeignKey("dbo.Topics", t => t.TopicId, cascadeDelete: true)
                .Index(t => t.TopicId)
                .Index(t => t.AuthorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "SubscriptionId", "dbo.Subscriptions");
            DropForeignKey("dbo.Subscriptions", "TopicId", "dbo.Topics");
            DropForeignKey("dbo.Subscriptions", "AuthorId", "dbo.Authors");
            DropForeignKey("dbo.Notifications", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Posts", "TopicId", "dbo.Topics");
            DropForeignKey("dbo.Topics", "AuthorId", "dbo.Authors");
            DropForeignKey("dbo.Posts", "AuthorId", "dbo.Authors");
            DropIndex("dbo.Subscriptions", new[] { "AuthorId" });
            DropIndex("dbo.Subscriptions", new[] { "TopicId" });
            DropIndex("dbo.Topics", new[] { "AuthorId" });
            DropIndex("dbo.Posts", new[] { "AuthorId" });
            DropIndex("dbo.Posts", new[] { "TopicId" });
            DropIndex("dbo.Notifications", new[] { "PostId" });
            DropIndex("dbo.Notifications", new[] { "SubscriptionId" });
            DropTable("dbo.Subscriptions");
            DropTable("dbo.Topics");
            DropTable("dbo.Posts");
            DropTable("dbo.Notifications");
            DropTable("dbo.Authors");
        }
    }
}
