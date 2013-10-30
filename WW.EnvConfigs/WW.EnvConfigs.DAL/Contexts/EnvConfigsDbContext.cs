using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WW.EnvConfigs.DAL.Configurations;
using WW.EnvConfigs.DataModels;


namespace WW.EnvConfigs.DAL.Contexts
{
    public class EnvConfigsDbContext : DbContext
    {

        public EnvConfigsDbContext()
            : base("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;

            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<EnvConfigsDbContext, EnvConfigsMigrationConfiguration>()
                );
        }

        public IDbSet<Build> Builds { get; set; } // Environments
        public IDbSet<EnvKey> EnvKeys { get; set; } // EnvKeys
        public IDbSet<EnvValue> EnvValues { get; set; } // EnvValues
        public IDbSet<Locale> Locales { get; set; } // Locales1
        public IDbSet<WWApp> WWApps { get; set; } // WWApps
        public IDbSet<WWFramework> WWFrameworks { get; set; } // WWFrameworks


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new LocalesConfiguration());
            modelBuilder.Configurations.Add(new BuildConfiguration());
            modelBuilder.Configurations.Add(new WWFrameworksConfiguration());
            modelBuilder.Configurations.Add(new WWAppsConfiguration());
            modelBuilder.Configurations.Add(new EnvKeyConfiguration());
            modelBuilder.Configurations.Add(new EnvValueConfiguration());
        }
    }
}
