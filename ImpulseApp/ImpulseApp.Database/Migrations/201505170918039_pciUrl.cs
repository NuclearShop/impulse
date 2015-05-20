namespace ImpulseApp.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pciUrl : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VideoUnits", "Image", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VideoUnits", "Image", c => c.Binary());
        }
    }
}
