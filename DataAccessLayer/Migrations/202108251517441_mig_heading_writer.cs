namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_heading_writer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Headings", "IsWriterHeading", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Headings", "IsWriterHeading");
        }
    }
}
