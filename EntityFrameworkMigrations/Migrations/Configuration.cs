namespace EntityFrameworkMigrations.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GlobalDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(GlobalDataContext context)
        {
            SeedIndustries(context);
            context.SaveChanges();
        }

        private void SeedIndustries(GlobalDataContext context)
        {
            InsertOrUpdateIndustry("Technology", context);
            InsertOrUpdateIndustry("Finance", context);
            InsertOrUpdateIndustry("Entertainment", context);
            InsertOrUpdateIndustry("Hospitality", context);
        }

        private void InsertOrUpdateIndustry(string name, GlobalDataContext context)
        {
            var industry = context.Industries.SingleOrDefault(x => x.Name == name);

            if (industry == null)
            {
                industry = new Industry()
                {
                    Name = name
                };

                context.Industries.Add(industry);
            }
        }
    }
}
