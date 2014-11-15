namespace ImpulseApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migr10 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AdSessions", "UserLocale", c => c.String());
            AlterColumn("dbo.AdSessions", "UserBrowser", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AdSessions", "UserBrowser", c => c.Int(nullable: false));
            AlterColumn("dbo.AdSessions", "UserLocale", c => c.Int(nullable: false));
        }
    }
}
