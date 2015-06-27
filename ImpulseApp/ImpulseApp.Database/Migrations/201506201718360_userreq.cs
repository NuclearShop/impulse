namespace ImpulseApp.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userreq : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SessionId = c.Int(nullable: false),
                        MainText = c.String(),
                        BaseUrl = c.String(),
                        AdditionalText = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdSessions", t => t.SessionId, cascadeDelete: true)
                .Index(t => t.SessionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRequests", "SessionId", "dbo.AdSessions");
            DropIndex("dbo.UserRequests", new[] { "SessionId" });
            DropTable("dbo.UserRequests");
        }
    }
}
