namespace ImpulseApp.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class timetofloat : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserElements", "TimeAppear", c => c.Single(nullable: false));
            AlterColumn("dbo.UserElements", "TimeDisappear", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserElements", "TimeDisappear", c => c.Int(nullable: false));
            AlterColumn("dbo.UserElements", "TimeAppear", c => c.Int(nullable: false));
        }
    }
}
