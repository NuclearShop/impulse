namespace ImpulseApp.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class defSlide : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdStates", "DefaultNext", c => c.Int(nullable: false));
            AddColumn("dbo.AdStates", "DefaultNextTime", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdStates", "DefaultNextTime");
            DropColumn("dbo.AdStates", "DefaultNext");
        }
    }
}
