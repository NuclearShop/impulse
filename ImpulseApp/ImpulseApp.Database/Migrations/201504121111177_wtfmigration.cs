namespace ImpulseApp.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wtfmigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdStates", "IsEnd", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdStates", "IsEnd");
        }
    }
}
