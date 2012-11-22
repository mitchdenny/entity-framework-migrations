using EntityFrameworkMigrations.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkMigrations
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                ShowUsage();
                return;
            }

            var method = args[0];

            switch (method.ToLower())
            {
                case "dbmigrator":
                    MigrateUsingDbMigrator();
                    break;

                case "initializer":
                    MigrateUsingInitializer();
                    break;

                default:
                    ShowUsage();
                    return;
            }
        }

        private static void MigrateUsingInitializer()
        {
            var initializer = new MigrateDatabaseToLatestVersion<GlobalDataContext, Configuration>("MigrationDemo");
            Database.SetInitializer<GlobalDataContext>(initializer);
            PrintSeededData();          
        }

        private static void MigrateUsingDbMigrator()
        {
            var configuration = GetConfiguration();

            var migrator = new DbMigrator(configuration);

            Console.WriteLine("Before Migration");
            PrintMigrationStatus(migrator);

            migrator.Update();

            Console.WriteLine("After Migration");
            PrintMigrationStatus(migrator);
            PrintSeededData();
        }

        private static void PrintMigrationStatus(DbMigrator migrator)
        {
            foreach (var applied in migrator.GetDatabaseMigrations())
            {
                Console.WriteLine("\tApplied: {0}", applied);
            }
            foreach (var pending in migrator.GetPendingMigrations())
            {
                Console.WriteLine("\tPending: {0}", pending);
            }
            Console.WriteLine();
        }

        private static void PrintSeededData()
        {
            Console.WriteLine("Seeded Data");
            using (var context = new GlobalDataContext("MigrationDemo"))
            {
                foreach (var industry in context.Industries)
                {
                    Console.WriteLine("Industry: {0}", industry.Name);
                }
            }
            Console.WriteLine();
        }

        private static Configuration GetConfiguration()
        {
            var connectionInfo = new DbConnectionInfo("MigrationDemo");
            var configuration = new Configuration();
            configuration.TargetDatabase = connectionInfo;
            return configuration;
        }

        private static void ShowUsage()
        {
            Console.WriteLine("Usage: EntityFrameworkMigrations.exe [method] [connection string]");
            Console.WriteLine();
            Console.WriteLine("\t[method] - dbmigrator, initializer");
            Console.WriteLine("\t[connection string] - self explanitory");
        }
    }
}
