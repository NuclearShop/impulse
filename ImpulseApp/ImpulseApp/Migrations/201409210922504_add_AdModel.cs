namespace ImpulseApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_AdModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SimpleAdModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShortUrlKey = c.String(),
                        HtmlSource = c.String(),
                        UserId = c.String(maxLength: 128),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SimpleAdModels", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.SimpleAdModels", new[] { "UserId" });
            DropTable("dbo.SimpleAdModels");
        }
    }
}
