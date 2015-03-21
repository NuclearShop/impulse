namespace ImpulseApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Clicks", "ActivityId", "dbo.Activities");
            DropForeignKey("dbo.Activities", "SessionId", "dbo.AdSessions");
            DropForeignKey("dbo.AdSessions", "AdId", "dbo.SimpleAdModels");
            DropForeignKey("dbo.SimpleAdModels", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Activities", new[] { "SessionId" });
            DropIndex("dbo.Clicks", new[] { "ActivityId" });
            DropIndex("dbo.AdSessions", new[] { "AdId" });
            DropIndex("dbo.SimpleAdModels", new[] { "UserId" });
            DropTable("dbo.Activities");
            DropTable("dbo.Clicks");
            DropTable("dbo.AdSessions");
            DropTable("dbo.SimpleAdModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SimpleAdModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ShortUrlKey = c.String(),
                        HtmlSource = c.String(),
                        UserId = c.String(maxLength: 128),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AdSessions",
                c => new
                    {
                        SessionId = c.Int(nullable: false, identity: true),
                        AdId = c.Int(nullable: false),
                        DateTimeStart = c.DateTime(nullable: false),
                        DateTimeEnd = c.DateTime(nullable: false),
                        ActiveMilliseconds = c.Int(nullable: false),
                        UserIp = c.String(),
                        UserLocation = c.String(),
                        UserLocale = c.String(),
                        UserBrowser = c.String(),
                    })
                .PrimaryKey(t => t.SessionId);
            
            CreateTable(
                "dbo.Clicks",
                c => new
                    {
                        ClickId = c.Int(nullable: false, identity: true),
                        ActivityId = c.Int(nullable: false),
                        ClickZone = c.String(),
                        ClickTime = c.DateTime(nullable: false),
                        ClickType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClickId);
            
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        SessionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.SimpleAdModels", "UserId");
            CreateIndex("dbo.AdSessions", "AdId");
            CreateIndex("dbo.Clicks", "ActivityId");
            CreateIndex("dbo.Activities", "SessionId");
            AddForeignKey("dbo.SimpleAdModels", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AdSessions", "AdId", "dbo.SimpleAdModels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Activities", "SessionId", "dbo.AdSessions", "SessionId", cascadeDelete: true);
            AddForeignKey("dbo.Clicks", "ActivityId", "dbo.Activities", "Id", cascadeDelete: true);
        }
    }
}
