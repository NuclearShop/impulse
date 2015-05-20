namespace ImpulseApp.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clickupd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clicks", "ClickNextTime", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clicks", "ClickNextTime");
        }
    }
}
