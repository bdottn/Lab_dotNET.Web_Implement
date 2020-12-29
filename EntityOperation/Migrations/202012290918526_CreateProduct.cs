namespace EntityOperation.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CreateProduct : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 30),
                    Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    CreatedTime = c.DateTime(nullable: false),
                    LatestModifiedTime = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropTable("dbo.Products");
        }
    }
}