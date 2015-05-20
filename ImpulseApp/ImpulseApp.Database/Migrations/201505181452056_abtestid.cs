namespace ImpulseApp.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abtestid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdSessions", "AbTestId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdSessions", "AbTestId");
        }
    }
}
