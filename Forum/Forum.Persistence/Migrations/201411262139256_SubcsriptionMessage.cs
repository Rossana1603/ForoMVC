namespace Forum.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubcsriptionMessage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subscriptions", "Message", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subscriptions", "Message");
        }
    }
}
