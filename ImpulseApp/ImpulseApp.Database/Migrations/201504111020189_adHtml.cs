namespace ImpulseApp.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adHtml : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SimpleAdModels", "HtmlStartSource", c => c.String());
            AddColumn("dbo.SimpleAdModels", "HtmlEndSource", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SimpleAdModels", "HtmlEndSource");
            DropColumn("dbo.SimpleAdModels", "HtmlStartSource");
        }
    }
}
