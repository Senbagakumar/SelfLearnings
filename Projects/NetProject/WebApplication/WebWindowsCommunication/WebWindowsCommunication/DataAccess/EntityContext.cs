using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using WebWindowsCommunication.Models;

namespace WebWindowsCommunication.DataAccess
{
    public class EntityContext : DbContext
    {
        public static string connectionString = ConfigurationManager.AppSettings["DbConnectionString"];
        public EntityContext() : base(connectionString)
        {
        }
        public DbSet<Settings> settings { get; set; }
        public DbSet<Wagon> wagons { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<EntityContext>(new MigrateDatabaseToLatestVersion<EntityContext, MyConfiguration>());

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;

            base.OnModelCreating(modelBuilder);

        }


    }

    public class MyConfiguration : System.Data.Entity.Migrations.DbMigrationsConfiguration<EntityContext>
    {
        public MyConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            ContextKey = "WebWindowsCommunication.DataAccess.EntityContext";
        }
    }
}