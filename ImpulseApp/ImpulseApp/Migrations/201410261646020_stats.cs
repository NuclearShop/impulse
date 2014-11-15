namespace ImpulseApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stats : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClickStats",
                c => new
                    {
                        ClickId = c.Int(nullable: false, identity: true),
                        SessionId = c.Int(nullable: false),
                        ClickZone = c.String(),
                        ClickTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ClickId)
                .ForeignKey("dbo.SimpleJStats", t => t.SessionId, cascadeDelete: true)
                .Index(t => t.SessionId);
            
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
                .PrimaryKey(t => t.SessionId)
                .ForeignKey("dbo.SimpleAdModels", t => t.AdId, cascadeDelete: true)
                .Index(t => t.AdId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClickStats", "SessionId", "dbo.SimpleJStats");
            DropForeignKey("dbo.SimpleJStats", "AdId", "dbo.SimpleAdModels");
            DropIndex("dbo.SimpleJStats", new[] { "AdId" });
            DropIndex("dbo.ClickStats", new[] { "SessionId" });
            DropTable("dbo.SimpleJStats");
            DropTable("dbo.ClickStats");
        }
    }
}
