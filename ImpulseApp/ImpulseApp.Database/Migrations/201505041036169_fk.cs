namespace ImpulseApp.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fk : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Versionings", "ChildAdId", "dbo.SimpleAdModels");
            DropForeignKey("dbo.Versionings", "RootAdId", "dbo.SimpleAdModels");
            DropForeignKey("dbo.AdSessions", "AdId", "dbo.SimpleAdModels");
            DropForeignKey("dbo.AdStates", "AdId", "dbo.SimpleAdModels");
            DropForeignKey("dbo.NodeLinks", "AdId", "dbo.SimpleAdModels");
            DropForeignKey("dbo.Activities", "SessionId", "dbo.AdSessions");
            DropForeignKey("dbo.Clicks", "ActivityId", "dbo.Activities");
            DropForeignKey("dbo.UserElements", "AdStateId", "dbo.AdStates");
            DropForeignKey("dbo.AdStates", "VideoUnitId", "dbo.VideoUnits");
            DropForeignKey("dbo.HtmlTags", "UserElementId", "dbo.UserElements");
            DropIndex("dbo.ABTests", new[] { "AdAId" });
            DropIndex("dbo.ABTests", new[] { "AdBId" });
            DropIndex("dbo.Versionings", new[] { "RootAdId" });
            DropIndex("dbo.Versionings", new[] { "ChildAdId" });
            AlterColumn("dbo.ABTests", "AdAId", c => c.Int());
            AlterColumn("dbo.ABTests", "AdBId", c => c.Int());
            CreateIndex("dbo.ABTests", "AdAId");
            CreateIndex("dbo.ABTests", "AdBId");
            AddForeignKey("dbo.AdSessions", "AdId", "dbo.SimpleAdModels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AdStates", "AdId", "dbo.SimpleAdModels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.NodeLinks", "AdId", "dbo.SimpleAdModels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Activities", "SessionId", "dbo.AdSessions", "SessionId", cascadeDelete: true);
            AddForeignKey("dbo.Clicks", "ActivityId", "dbo.Activities", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserElements", "AdStateId", "dbo.AdStates", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AdStates", "VideoUnitId", "dbo.VideoUnits", "Id", cascadeDelete: true);
            AddForeignKey("dbo.HtmlTags", "UserElementId", "dbo.UserElements", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HtmlTags", "UserElementId", "dbo.UserElements");
            DropForeignKey("dbo.AdStates", "VideoUnitId", "dbo.VideoUnits");
            DropForeignKey("dbo.UserElements", "AdStateId", "dbo.AdStates");
            DropForeignKey("dbo.Clicks", "ActivityId", "dbo.Activities");
            DropForeignKey("dbo.Activities", "SessionId", "dbo.AdSessions");
            DropForeignKey("dbo.NodeLinks", "AdId", "dbo.SimpleAdModels");
            DropForeignKey("dbo.AdStates", "AdId", "dbo.SimpleAdModels");
            DropForeignKey("dbo.AdSessions", "AdId", "dbo.SimpleAdModels");
            DropIndex("dbo.ABTests", new[] { "AdBId" });
            DropIndex("dbo.ABTests", new[] { "AdAId" });
            AlterColumn("dbo.ABTests", "AdBId", c => c.Int(nullable: false));
            AlterColumn("dbo.ABTests", "AdAId", c => c.Int(nullable: false));
            CreateIndex("dbo.Versionings", "ChildAdId");
            CreateIndex("dbo.Versionings", "RootAdId");
            CreateIndex("dbo.ABTests", "AdBId");
            CreateIndex("dbo.ABTests", "AdAId");
            AddForeignKey("dbo.HtmlTags", "UserElementId", "dbo.UserElements", "Id");
            AddForeignKey("dbo.AdStates", "VideoUnitId", "dbo.VideoUnits", "Id");
            AddForeignKey("dbo.UserElements", "AdStateId", "dbo.AdStates", "Id");
            AddForeignKey("dbo.Clicks", "ActivityId", "dbo.Activities", "Id");
            AddForeignKey("dbo.Activities", "SessionId", "dbo.AdSessions", "SessionId");
            AddForeignKey("dbo.NodeLinks", "AdId", "dbo.SimpleAdModels", "Id");
            AddForeignKey("dbo.AdStates", "AdId", "dbo.SimpleAdModels", "Id");
            AddForeignKey("dbo.AdSessions", "AdId", "dbo.SimpleAdModels", "Id");
            AddForeignKey("dbo.Versionings", "RootAdId", "dbo.SimpleAdModels", "Id");
            AddForeignKey("dbo.Versionings", "ChildAdId", "dbo.SimpleAdModels", "Id");
        }
    }
}
