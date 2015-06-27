namespace ImpulseApp.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class actionurl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserElements", "ActionUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserElements", "ActionUrl");
        }
    }
}
