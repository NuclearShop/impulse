namespace ImpulseApp.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userelems : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserElements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HtmlId = c.String(),
                        HtmlClass = c.String(),
                        UseDefaultStyle = c.Boolean(nullable: false),
                        HtmlStyle = c.String(),
                        Text = c.String(),
                        X = c.Int(nullable: false),
                        Y = c.Int(nullable: false),
                        Width = c.String(),
                        Height = c.String(),
                        Action = c.String(),
                        TimeAppear = c.Int(nullable: false),
                        TimeDisappear = c.Int(nullable: false),
                        CurrentId = c.Int(nullable: false),
                        NextId = c.Int(nullable: false),
                        NextTime = c.Int(nullable: false),
                        AdStateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdStates", t => t.AdStateId, cascadeDelete: true)
                .Index(t => t.AdStateId);
            
            CreateTable(
                "dbo.ModeratorViews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ViewDateTime = c.DateTime(nullable: false),
                        AdId = c.Int(nullable: false),
                        IsReviewed = c.Boolean(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        Review = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserElements", "AdStateId", "dbo.AdStates");
            DropIndex("dbo.UserElements", new[] { "AdStateId" });
            DropTable("dbo.ModeratorViews");
            DropTable("dbo.UserElements");
        }
    }
}
