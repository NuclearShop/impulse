namespace ImpulseApp.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class versioning : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Versionings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RootAdId = c.Int(nullable: false),
                        ChildAdId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SimpleAdModels", t => t.ChildAdId, cascadeDelete: false)
                .ForeignKey("dbo.SimpleAdModels", t => t.RootAdId, cascadeDelete: false)
                .Index(t => t.RootAdId)
                .Index(t => t.ChildAdId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Versionings", "RootAdId", "dbo.SimpleAdModels");
            DropForeignKey("dbo.Versionings", "ChildAdId", "dbo.SimpleAdModels");
            DropIndex("dbo.Versionings", new[] { "ChildAdId" });
            DropIndex("dbo.Versionings", new[] { "RootAdId" });
            DropTable("dbo.Versionings");
        }
    }
}
