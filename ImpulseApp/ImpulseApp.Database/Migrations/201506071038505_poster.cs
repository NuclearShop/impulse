namespace ImpulseApp.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class poster : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SimpleAdModels", "Poster", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SimpleAdModels", "Poster");
        }
    }
}
