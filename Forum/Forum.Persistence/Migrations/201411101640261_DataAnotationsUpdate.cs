namespace Forum.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataAnotationsUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Authors", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Topics", "Title", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Topics", "Content", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Topics", "Content", c => c.String());
            AlterColumn("dbo.Topics", "Title", c => c.String());
            AlterColumn("dbo.Authors", "Name", c => c.String());
        }
    }
}
