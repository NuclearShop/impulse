namespace ImpulseApp.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class image : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VideoUnits", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.VideoUnits", "Image");
        }
    }
}
