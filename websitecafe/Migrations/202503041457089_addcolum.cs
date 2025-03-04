namespace websitecafe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "View", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "View");
        }
    }
}
