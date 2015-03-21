namespace ImpulseApp.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VideoUnits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateLoaded = c.DateTime(nullable: false),
                        Name = c.String(),
                        GeneratedName = c.String(),
                        UserName = c.String(),
                        Length = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.SimpleAdModels", "JsSource", c => c.String());
            AddColumn("dbo.AdStates", "StartTime", c => c.Int(nullable: false));
            AddColumn("dbo.AdStates", "EndTime", c => c.Int(nullable: false));
            AddColumn("dbo.AdStates", "HtmlSource", c => c.String());
            AddColumn("dbo.AdStates", "VideoUnitId", c => c.Int(nullable: false));
            CreateIndex("dbo.AdStates", "VideoUnitId");
            AddForeignKey("dbo.AdStates", "VideoUnitId", "dbo.VideoUnits", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AdStates", "VideoUnitId", "dbo.VideoUnits");
            DropIndex("dbo.AdStates", new[] { "VideoUnitId" });
            DropColumn("dbo.AdStates", "VideoUnitId");
            DropColumn("dbo.AdStates", "HtmlSource");
            DropColumn("dbo.AdStates", "EndTime");
            DropColumn("dbo.AdStates", "StartTime");
            DropColumn("dbo.SimpleAdModels", "JsSource");
            DropTable("dbo.VideoUnits");
        }
    }
}
