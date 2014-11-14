namespace Forum.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignKeys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "Author_Id", "dbo.Authors");
            DropForeignKey("dbo.Posts", "Topic_Id", "dbo.Topics");
            DropForeignKey("dbo.Topics", "Author_Id", "dbo.Authors");
            DropIndex("dbo.Posts", new[] { "Author_Id" });
            DropIndex("dbo.Posts", new[] { "Topic_Id" });
            DropIndex("dbo.Topics", new[] { "Author_Id" });
            RenameColumn(table: "dbo.Posts", name: "Author_Id", newName: "AuthorId");
            RenameColumn(table: "dbo.Posts", name: "Topic_Id", newName: "TopicId");
            RenameColumn(table: "dbo.Topics", name: "Author_Id", newName: "AuthorId");
            AlterColumn("dbo.Posts", "AuthorId", c => c.Int(nullable: false));
            AlterColumn("dbo.Posts", "TopicId", c => c.Int(nullable: false));
            AlterColumn("dbo.Topics", "AuthorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Posts", "TopicId");
            CreateIndex("dbo.Posts", "AuthorId");
            CreateIndex("dbo.Topics", "AuthorId");
            AddForeignKey("dbo.Posts", "AuthorId", "dbo.Authors", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Posts", "TopicId", "dbo.Topics", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Topics", "AuthorId", "dbo.Authors", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Topics", "AuthorId", "dbo.Authors");
            DropForeignKey("dbo.Posts", "TopicId", "dbo.Topics");
            DropForeignKey("dbo.Posts", "AuthorId", "dbo.Authors");
            DropIndex("dbo.Topics", new[] { "AuthorId" });
            DropIndex("dbo.Posts", new[] { "AuthorId" });
            DropIndex("dbo.Posts", new[] { "TopicId" });
            AlterColumn("dbo.Topics", "AuthorId", c => c.Int());
            AlterColumn("dbo.Posts", "TopicId", c => c.Int());
            AlterColumn("dbo.Posts", "AuthorId", c => c.Int());
            RenameColumn(table: "dbo.Topics", name: "AuthorId", newName: "Author_Id");
            RenameColumn(table: "dbo.Posts", name: "TopicId", newName: "Topic_Id");
            RenameColumn(table: "dbo.Posts", name: "AuthorId", newName: "Author_Id");
            CreateIndex("dbo.Topics", "Author_Id");
            CreateIndex("dbo.Posts", "Topic_Id");
            CreateIndex("dbo.Posts", "Author_Id");
            AddForeignKey("dbo.Topics", "Author_Id", "dbo.Authors", "Id");
            AddForeignKey("dbo.Posts", "Topic_Id", "dbo.Topics", "Id");
            AddForeignKey("dbo.Posts", "Author_Id", "dbo.Authors", "Id");
        }
    }
}
