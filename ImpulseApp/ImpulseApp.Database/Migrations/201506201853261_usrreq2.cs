namespace ImpulseApp.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usrreq2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserRequests", "SessionId", "dbo.AdSessions");
            DropIndex("dbo.UserRequests", new[] { "SessionId" });
            AddColumn("dbo.UserRequests", "AdId", c => c.Int(nullable: false));
            CreateIndex("dbo.UserRequests", "AdId");
            AddForeignKey("dbo.UserRequests", "AdId", "dbo.SimpleAdModels", "Id", cascadeDelete: true);
            DropColumn("dbo.UserRequests", "SessionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserRequests", "SessionId", c => c.Int(nullable: false));
            DropForeignKey("dbo.UserRequests", "AdId", "dbo.SimpleAdModels");
            DropIndex("dbo.UserRequests", new[] { "AdId" });
            DropColumn("dbo.UserRequests", "AdId");
            CreateIndex("dbo.UserRequests", "SessionId");
            AddForeignKey("dbo.UserRequests", "SessionId", "dbo.AdSessions", "SessionId", cascadeDelete: true);
        }
    }
}
