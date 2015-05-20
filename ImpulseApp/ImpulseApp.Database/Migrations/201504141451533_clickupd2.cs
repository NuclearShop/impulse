namespace ImpulseApp.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clickupd2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clicks", "ClickStamp", c => c.String());
            AlterColumn("dbo.Clicks", "ClickType", c => c.String());
            AlterColumn("dbo.Clicks", "ClickText", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clicks", "ClickText", c => c.Int(nullable: false));
            AlterColumn("dbo.Clicks", "ClickType", c => c.Int(nullable: false));
            DropColumn("dbo.Clicks", "ClickStamp");
        }
    }
}
