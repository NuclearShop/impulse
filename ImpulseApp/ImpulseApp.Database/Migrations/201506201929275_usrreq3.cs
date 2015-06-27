namespace ImpulseApp.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usrreq3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserRequests", "UserIp", c => c.String());
            AddColumn("dbo.UserRequests", "DateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserRequests", "DateTime");
            DropColumn("dbo.UserRequests", "UserIp");
        }
    }
}
