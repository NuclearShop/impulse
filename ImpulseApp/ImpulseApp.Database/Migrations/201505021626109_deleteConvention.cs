namespace ImpulseApp.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteConvention : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Clicks", "ActivityId", "dbo.Activities");
            DropForeignKey("dbo.Activities", "SessionId", "dbo.AdSessions");
            DropForeignKey("dbo.AdSessions", "AdId", "dbo.SimpleAdModels");
            DropForeignKey("dbo.AdStates", "AdId", "dbo.SimpleAdModels");
            DropForeignKey("dbo.NodeLinks", "AdId", "dbo.SimpleAdModels");
            DropForeignKey("dbo.UserElements", "AdStateId", "dbo.AdStates");
            DropForeignKey("dbo.AdStates", "VideoUnitId", "dbo.VideoUnits");
            DropForeignKey("dbo.HtmlTags", "UserElementId", "dbo.UserElements");
            DropForeignKey("dbo.Versionings", "ChildAdId", "dbo.SimpleAdModels");
            DropForeignKey("dbo.Versionings", "RootAdId", "dbo.SimpleAdModels");
            AddForeignKey("dbo.Clicks", "ActivityId", "dbo.Activities", "Id");
            AddForeignKey("dbo.Activities", "SessionId", "dbo.AdSessions", "SessionId");
            AddForeignKey("dbo.AdSessions", "AdId", "dbo.SimpleAdModels", "Id");
            AddForeignKey("dbo.AdStates", "AdId", "dbo.SimpleAdModels", "Id");
            AddForeignKey("dbo.NodeLinks", "AdId", "dbo.SimpleAdModels", "Id");
            AddForeignKey("dbo.UserElements", "AdStateId", "dbo.AdStates", "Id");
            AddForeignKey("dbo.AdStates", "VideoUnitId", "dbo.VideoUnits", "Id");
            AddForeignKey("dbo.HtmlTags", "UserElementId", "dbo.UserElements", "Id");
            AddForeignKey("dbo.Versionings", "ChildAdId", "dbo.SimpleAdModels", "Id");
            AddForeignKey("dbo.Versionings", "RootAdId", "dbo.SimpleAdModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Versionings", "RootAdId", "dbo.SimpleAdModels");
            DropForeignKey("dbo.Versionings", "ChildAdId", "dbo.SimpleAdModels");
            DropForeignKey("dbo.HtmlTags", "UserElementId", "dbo.UserElements");
            DropForeignKey("dbo.AdStates", "VideoUnitId", "dbo.VideoUnits");
            DropForeignKey("dbo.UserElements", "AdStateId", "dbo.AdStates");
            DropForeignKey("dbo.NodeLinks", "AdId", "dbo.SimpleAdModels");
            DropForeignKey("dbo.AdStates", "AdId", "dbo.SimpleAdModels");
            DropForeignKey("dbo.AdSessions", "AdId", "dbo.SimpleAdModels");
            DropForeignKey("dbo.Activities", "SessionId", "dbo.AdSessions");
            DropForeignKey("dbo.Clicks", "ActivityId", "dbo.Activities");
            AddForeignKey("dbo.Versionings", "RootAdId", "dbo.SimpleAdModels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Versionings", "ChildAdId", "dbo.SimpleAdModels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.HtmlTags", "UserElementId", "dbo.UserElements", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AdStates", "VideoUnitId", "dbo.VideoUnits", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserElements", "AdStateId", "dbo.AdStates", "Id", cascadeDelete: true);
            AddForeignKey("dbo.NodeLinks", "AdId", "dbo.SimpleAdModels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AdStates", "AdId", "dbo.SimpleAdModels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AdSessions", "AdId", "dbo.SimpleAdModels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Activities", "SessionId", "dbo.AdSessions", "SessionId", cascadeDelete: true);
            AddForeignKey("dbo.Clicks", "ActivityId", "dbo.Activities", "Id", cascadeDelete: true);
        }
    }
}
