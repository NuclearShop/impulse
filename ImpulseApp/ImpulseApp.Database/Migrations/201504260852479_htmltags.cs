namespace ImpulseApp.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class htmltags : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HtmlTags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Value = c.String(),
                        UserElementId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserElements", t => t.UserElementId, cascadeDelete: true)
                .Index(t => t.UserElementId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HtmlTags", "UserElementId", "dbo.UserElements");
            DropIndex("dbo.HtmlTags", new[] { "UserElementId" });
            DropTable("dbo.HtmlTags");
        }
    }
}
