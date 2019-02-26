using SelfAssessment.Models;
using SelfAssessment.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace SelfAssessment.DataAccess
{
    public class AssessmentContext : DbContext
    {
        public static string connectionString = ConfigurationManager.AppSettings["DbConnectionString"];
        public AssessmentContext() : base(connectionString)
        {
        }
        public DbSet<Organization> UserInfo { get; set; }  
        public DbSet<Others> others { get; set; }
        public DbSet<State> states { get; set; }
        public DbSet<ServiceType> serviceTypes { get; set; }
        public DbSet<City> cities { get; set; }
        public DbSet<Revenue> revenues { get; set; }
        public DbSet<Sector> sectors { get; set; }
        public DbSet<SubSector> subSectors { get; set; }
        public DbSet<Group> groups { get; set; }
        public DbSet<Assessment> assessments { get; set; }
        public DbSet<Questions> questions { get; set; }
        public DbSet<OrganizationLevelHistory> organizationLevelHistories { get; set; }
        public DbSet<AssessmentLevelMapping> assessmentLevelMappings { get; set; }
        public DbSet<Answer> answers { get; set; }
        public DbSet<Log> logs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           // modelBuilder.Entity<SubSector>()
           //.HasRequired<Sector>(s => s.SelectedSector)
           //.WithMany(s => s.SubSectors);

            Database.SetInitializer<AssessmentContext>(new MigrateDatabaseToLatestVersion<AssessmentContext, MyConfiguration>());

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;

            base.OnModelCreating(modelBuilder);

        }
    }
    public class MyConfiguration : System.Data.Entity.Migrations.DbMigrationsConfiguration<AssessmentContext>
    {
        public MyConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            ContextKey = "SelfAssessment.DataAccess.AssessmentContext";
        }
    }
}