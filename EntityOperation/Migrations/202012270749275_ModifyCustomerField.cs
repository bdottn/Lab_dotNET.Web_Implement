namespace EntityOperation.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ModifyCustomerField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Phone", c => c.String(maxLength: 24));
            AddColumn("dbo.Customers", "CreatedTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Customers", "LatestModifiedTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Customers", "Name", c => c.String(nullable: false, maxLength: 30));
        }

        public override void Down()
        {
            AlterColumn("dbo.Customers", "Name", c => c.String());
            DropColumn("dbo.Customers", "LatestModifiedTime");
            DropColumn("dbo.Customers", "CreatedTime");
            DropColumn("dbo.Customers", "Phone");
        }
    }
}