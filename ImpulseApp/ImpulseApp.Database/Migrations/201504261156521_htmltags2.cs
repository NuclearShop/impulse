namespace ImpulseApp.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class htmltags2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserElements", "HtmlType", c => c.String());
            AddColumn("dbo.UserElements", "FormName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserElements", "FormName");
            DropColumn("dbo.UserElements", "HtmlType");
        }
    }
}
