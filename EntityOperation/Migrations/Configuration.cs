namespace EntityOperation.Migrations
{
    using System.Data.Entity.Migrations;

    sealed class Configuration : DbMigrationsConfiguration<LabContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}