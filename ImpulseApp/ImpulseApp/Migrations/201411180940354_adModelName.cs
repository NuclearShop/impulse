namespace ImpulseApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adModelName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SimpleAdModels", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SimpleAdModels", "Name");
        }
    }
}
