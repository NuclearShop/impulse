namespace ImpulseApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migr_stats2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Clicks", "ActivityId", "dbo.Activities");
            DropIndex("dbo.Clicks", new[] { "ActivityId" });
            DropPrimaryKey("dbo.Activities");
            DropPrimaryKey("dbo.Clicks");
            AlterColumn("dbo.Activities", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Clicks", "ClickId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Clicks", "ActivityId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Activities", "Id");
            AddPrimaryKey("dbo.Clicks", "ClickId");
            CreateIndex("dbo.Clicks", "ActivityId");
            AddForeignKey("dbo.Clicks", "ActivityId", "dbo.Activities", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clicks", "ActivityId", "dbo.Activities");
            DropIndex("dbo.Clicks", new[] { "ActivityId" });
            DropPrimaryKey("dbo.Clicks");
            DropPrimaryKey("dbo.Activities");
            AlterColumn("dbo.Clicks", "ActivityId", c => c.Int(nullable: false));
            AlterColumn("dbo.Clicks", "ClickId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Activities", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Clicks", "ClickId");
            AddPrimaryKey("dbo.Activities", "Id");
            CreateIndex("dbo.Clicks", "ActivityId");
            AddForeignKey("dbo.Clicks", "ActivityId", "dbo.Activities", "Id", cascadeDelete: true);
        }
    }
}
