namespace ImpulseApp.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addStateUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdStates", "ChainedHtml", c => c.String());
            AddColumn("dbo.AdStates", "IsFullPlay", c => c.Boolean(nullable: false));
            AddColumn("dbo.AdStates", "IsStart", c => c.Boolean(nullable: false));
            AddColumn("dbo.VideoUnits", "MimeType", c => c.String());
            DropColumn("dbo.AdStates", "StartTime");
            DropColumn("dbo.AdStates", "HtmlSource");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AdStates", "HtmlSource", c => c.String());
            AddColumn("dbo.AdStates", "StartTime", c => c.Int(nullable: false));
            DropColumn("dbo.VideoUnits", "MimeType");
            DropColumn("dbo.AdStates", "IsStart");
            DropColumn("dbo.AdStates", "IsFullPlay");
            DropColumn("dbo.AdStates", "ChainedHtml");
        }
    }
}
