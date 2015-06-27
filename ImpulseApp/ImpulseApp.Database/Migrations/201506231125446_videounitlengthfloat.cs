namespace ImpulseApp.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class videounitlengthfloat : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VideoUnits", "Length", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VideoUnits", "Length", c => c.Int(nullable: false));
        }
    }
}
