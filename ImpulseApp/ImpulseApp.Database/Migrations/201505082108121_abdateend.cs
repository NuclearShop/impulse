namespace ImpulseApp.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abdateend : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ABTests", "DateEnd", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ABTests", "DateEnd");
        }
    }
}
