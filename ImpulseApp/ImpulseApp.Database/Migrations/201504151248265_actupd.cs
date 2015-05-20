namespace ImpulseApp.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class actupd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activities", "CurrentStateName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Activities", "CurrentStateName");
        }
    }
}
