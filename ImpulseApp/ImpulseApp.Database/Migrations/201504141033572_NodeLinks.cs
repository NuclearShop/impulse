namespace ImpulseApp.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NodeLinks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NodeLinks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdId = c.Int(nullable: false),
                        V1 = c.Int(nullable: false),
                        V2 = c.Int(nullable: false),
                        T = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SimpleAdModels", t => t.AdId, cascadeDelete: true)
                .Index(t => t.AdId);
            
            AddColumn("dbo.Clicks", "ClickCurrentStage", c => c.Int(nullable: false));
            AddColumn("dbo.Clicks", "ClickNextStage", c => c.Int(nullable: false));
            AddColumn("dbo.Clicks", "ClickText", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NodeLinks", "AdId", "dbo.SimpleAdModels");
            DropIndex("dbo.NodeLinks", new[] { "AdId" });
            DropColumn("dbo.Clicks", "ClickText");
            DropColumn("dbo.Clicks", "ClickNextStage");
            DropColumn("dbo.Clicks", "ClickCurrentStage");
            DropTable("dbo.NodeLinks");
        }
    }
}
