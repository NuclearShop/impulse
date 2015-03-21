namespace ImpulseApp.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class videopath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VideoUnits", "FullPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.VideoUnits", "FullPath");
        }
    }
}
