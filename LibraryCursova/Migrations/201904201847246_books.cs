namespace LibraryCursova.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class books : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Count", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "Count");
        }
    }
}
