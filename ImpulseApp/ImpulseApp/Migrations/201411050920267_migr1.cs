namespace ImpulseApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migr1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SimpleJStats", "AdId", "dbo.SimpleAdModels");
            DropForeignKey("dbo.ClickStats", "SessionId", "dbo.SimpleJStats");
            DropIndex("dbo.ClickStats", new[] { "SessionId" });
            DropIndex("dbo.SimpleJStats", new[] { "AdId" });
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        SessionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdSessions", t => t.SessionId, cascadeDelete: true)
                .Index(t => t.SessionId);
            
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
                        UserLocale = c.Int(nullable: false),
                        UserBrowser = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SessionId)
                .ForeignKey("dbo.SimpleAdModels", t => t.AdId, cascadeDelete: true)
                .Index(t => t.AdId);
            
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
                .PrimaryKey(t => t.ClickId)
                .ForeignKey("dbo.Activities", t => t.ActivityId, cascadeDelete: true)
                .Index(t => t.ActivityId);
            
            DropTable("dbo.ClickStats");
            DropTable("dbo.SimpleJStats");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SimpleJStats",
                c => new
                    {
                        SessionId = c.Int(nullable: false, identity: true),
                        AdId = c.Int(nullable: false),
                        SessionStarted = c.DateTime(nullable: false),
                        SessionEnded = c.DateTime(nullable: false),
                        ActiveMilliseconds = c.Int(nullable: false),
                        UserIp = c.String(),
                        UserLocation = c.String(),
                        UserLocale = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SessionId);
            
            CreateTable(
                "dbo.ClickStats",
                c => new
                    {
                        ClickId = c.Int(nullable: false, identity: true),
                        SessionId = c.Int(nullable: false),
                        ClickZone = c.String(),
                        ClickTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ClickId);
            
            DropForeignKey("dbo.Clicks", "ActivityId", "dbo.Activities");
            DropForeignKey("dbo.Activities", "SessionId", "dbo.AdSessions");
            DropForeignKey("dbo.AdSessions", "AdId", "dbo.SimpleAdModels");
            DropIndex("dbo.Clicks", new[] { "ActivityId" });
            DropIndex("dbo.AdSessions", new[] { "AdId" });
            DropIndex("dbo.Activities", new[] { "SessionId" });
            DropTable("dbo.Clicks");
            DropTable("dbo.AdSessions");
            DropTable("dbo.Activities");
            CreateIndex("dbo.SimpleJStats", "AdId");
            CreateIndex("dbo.ClickStats", "SessionId");
            AddForeignKey("dbo.ClickStats", "SessionId", "dbo.SimpleJStats", "SessionId", cascadeDelete: true);
            AddForeignKey("dbo.SimpleJStats", "AdId", "dbo.SimpleAdModels", "Id", cascadeDelete: true);
        }
    }
}
