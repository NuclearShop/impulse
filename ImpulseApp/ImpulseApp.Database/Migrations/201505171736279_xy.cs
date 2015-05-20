namespace ImpulseApp.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdStates", "X", c => c.Int(nullable: false));
            AddColumn("dbo.AdStates", "Y", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdStates", "Y");
            DropColumn("dbo.AdStates", "X");
        }
    }
}
