namespace ImpulseApp.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstState : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SimpleAdModels", "FirstState", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SimpleAdModels", "FirstState");
        }
    }
}
