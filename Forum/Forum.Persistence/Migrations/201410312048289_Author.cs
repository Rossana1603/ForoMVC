namespace Forum.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Author : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Posts", "Author_Id", c => c.Int());
            AddColumn("dbo.Topics", "Author_Id", c => c.Int());
            CreateIndex("dbo.Posts", "Author_Id");
            CreateIndex("dbo.Topics", "Author_Id");
            AddForeignKey("dbo.Posts", "Author_Id", "dbo.Authors", "Id");
            AddForeignKey("dbo.Topics", "Author_Id", "dbo.Authors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Topics", "Author_Id", "dbo.Authors");
            DropForeignKey("dbo.Posts", "Author_Id", "dbo.Authors");
            DropIndex("dbo.Topics", new[] { "Author_Id" });
            DropIndex("dbo.Posts", new[] { "Author_Id" });
            DropColumn("dbo.Topics", "Author_Id");
            DropColumn("dbo.Posts", "Author_Id");
            DropTable("dbo.Authors");
        }
    }
}
