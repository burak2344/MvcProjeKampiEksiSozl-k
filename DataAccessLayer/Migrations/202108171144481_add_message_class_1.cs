namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_message_class_1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "MessageContent", c => c.String());
            DropColumn("dbo.Messages", "MessageContet");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "MessageContet", c => c.String());
            DropColumn("dbo.Messages", "MessageContent");
        }
    }
}
