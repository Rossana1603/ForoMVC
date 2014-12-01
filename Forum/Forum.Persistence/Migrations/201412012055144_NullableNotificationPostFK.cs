namespace Forum.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableNotificationPostFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Notifications", "PostId", "dbo.Posts");
            DropIndex("dbo.Notifications", new[] { "PostId" });
            AddColumn("dbo.Notifications", "Post_Id", c => c.Int());
            AlterColumn("dbo.Notifications", "PostId", c => c.Int());
            CreateIndex("dbo.Notifications", "Post_Id");
            AddForeignKey("dbo.Notifications", "Post_Id", "dbo.Posts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "Post_Id", "dbo.Posts");
            DropIndex("dbo.Notifications", new[] { "Post_Id" });
            AlterColumn("dbo.Notifications", "PostId", c => c.Int(nullable: false));
            DropColumn("dbo.Notifications", "Post_Id");
            CreateIndex("dbo.Notifications", "PostId");
            AddForeignKey("dbo.Notifications", "PostId", "dbo.Posts", "Id");
        }
    }
}
