namespace ImpulseApp.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abtest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ABTests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdAId = c.Int(nullable: false),
                        AdBId = c.Int(nullable: false),
                        DateStart = c.DateTime(nullable: false),
                        ChangeHours = c.Int(nullable: false),
                        ChangeCount = c.Int(nullable: false),
                        ActiveAd = c.Int(nullable: false),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SimpleAdModels", t => t.AdAId)
                .ForeignKey("dbo.SimpleAdModels", t => t.AdBId)
                .Index(t => t.AdAId)
                .Index(t => t.AdBId);
            
            AddColumn("dbo.SimpleAdModels", "IsRoot", c => c.Boolean(nullable: false));
            AddColumn("dbo.SimpleAdModels", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ABTests", "AdBId", "dbo.SimpleAdModels");
            DropForeignKey("dbo.ABTests", "AdAId", "dbo.SimpleAdModels");
            DropIndex("dbo.ABTests", new[] { "AdBId" });
            DropIndex("dbo.ABTests", new[] { "AdAId" });
            DropColumn("dbo.SimpleAdModels", "IsActive");
            DropColumn("dbo.SimpleAdModels", "IsRoot");
            DropTable("dbo.ABTests");
        }
    }
}
