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
            if (args.Length != 2)
            {
                ShowUsage();
                return;
            }

            var method = args[0];
            var connectionString = args[1];

            switch (method.ToLower())
            {
                case "dbmigrator":
                    MigrateUsingDbMigrator(connectionString);
                    break;

                case "initializer":
                    MigrateUsingInitializer(connectionString);
                    break;

                default:
                    ShowUsage();
                    return;
            }
        }

        private static void MigrateUsingInitializer(string connectionString)
        {
            throw new NotImplementedException();
        }

        private static void MigrateUsingDbMigrator(string connectionString)
        {
            var configuration = GetConfiguration(connectionString);

            var migrator = new DbMigrator(configuration);

            Console.WriteLine("Before Migration");
            foreach (var applied in migrator.GetDatabaseMigrations())
            {
                Console.WriteLine("\tApplied: {0}", applied);
            }
            foreach (var pending in migrator.GetPendingMigrations())
            {
                Console.WriteLine("\tPending: {0}", pending);
            }
            Console.WriteLine();

            migrator.Update();

            Console.WriteLine("After Migration");
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

        private static Configuration GetConfiguration(string connectionString)
        {
            var connectionInfo = new DbConnectionInfo(connectionString, "System.Data.SqlClient");
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
