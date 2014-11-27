namespace Forum.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableNotificationDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "Message", c => c.String());
            AlterColumn("dbo.Notifications", "NotificationDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Notifications", "NotificationDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Notifications", "Message");
        }
    }
}
