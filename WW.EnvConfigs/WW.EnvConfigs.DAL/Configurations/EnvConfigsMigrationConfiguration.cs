using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Linq;
using WW.EnvConfigs.DAL;
using WW.EnvConfigs.DAL.Contexts;
using WW.EnvConfigs.DataModels;

namespace WW.EnvConfigs.DAL.Configurations
{
    public class EnvConfigsMigrationConfiguration : DbMigrationsConfiguration<EnvConfigsDbContext>
    {
        public EnvConfigsMigrationConfiguration()
        {
            bool boolAutomaticMigrationDataLossAllowed = false;            
            string strAutomaticMigrationDataLossAllowed = "false";
            if(ConfigurationManager.AppSettings["EF_AutomaticMigrationDataLossAllowed"] != null)
            {
                strAutomaticMigrationDataLossAllowed = ConfigurationManager.AppSettings["EF_AutomaticMigrationDataLossAllowed"].ToString();
            }
            bool.TryParse(strAutomaticMigrationDataLossAllowed, out boolAutomaticMigrationDataLossAllowed);
            
            bool boolAutomaticMigrationsEnabled = false;
            string strAutomaticMigrationsEnabled = "false";
            if (ConfigurationManager.AppSettings["EF_AutomaticMigrationsEnabled"] != null)
            {
                strAutomaticMigrationsEnabled = ConfigurationManager.AppSettings["EF_AutomaticMigrationDataLossAllowed"].ToString();
            }
            bool.TryParse(strAutomaticMigrationsEnabled, out boolAutomaticMigrationsEnabled);
           
            if (boolAutomaticMigrationDataLossAllowed)
            {
                this.AutomaticMigrationDataLossAllowed = true; //to be removed later on
            }
            else
            {
                this.AutomaticMigrationDataLossAllowed = false;
            }
            if (boolAutomaticMigrationsEnabled)
            {
                this.AutomaticMigrationsEnabled = true;
            }
            else
            {
                this.AutomaticMigrationsEnabled = false;
            }

        }

        protected override void Seed(EnvConfigsDbContext context)
        {
            base.Seed(context);

            //CreateUniqueConstraint(context);
            //SeedLocales(context);
            //SeedBuilds(context);
            //SeedAppsAndFrameworks(context);
            
        }

        private void CreateUniqueConstraint(EnvConfigsDbContext context)
        {
            AddUniqueConstraint(Constants.TABLE_NM_LOCALES, "ShortName", context);
            AddUniqueConstraint(Constants.TABLE_NM_LOCALES, "SiteId", context);

            AddUniqueConstraint(Constants.TABLE_NM_BUILDS, "Name", context);
            AddUniqueConstraint(Constants.TABLE_NM_WWFRAMEWORKS, "Name", context);
            AddUniqueConstraint(Constants.TABLE_NM_WWAPPS, "Name", context);
            //AddUniqueConstraint(Constants.TABLE_NM_ENVKEYS, "KeyName", context);
            AddUniqueConstraintForEnvKeyTable(Constants.TABLE_NM_ENVKEYS, context);

        }

        private void AddUniqueConstraint(string tableName, string columnName, EnvConfigsDbContext context)
        {
            var script = string.Format("ALTER TABLE [{0}] ADD CONSTRAINT uc_{0}_{1} UNIQUE ([{1}])", tableName, columnName);
            context.Database.ExecuteSqlCommand(script);
        }
        private void AddUniqueConstraintForEnvKeyTable(string tableName, EnvConfigsDbContext context)
        {
            var script = string.Format("ALTER TABLE [{0}] ADD CONSTRAINT uc_{0}_KEY_NAME_FWK_ID UNIQUE ([WWFrameworkId],[KeyName])", tableName);
            context.Database.ExecuteSqlCommand(script);
        }

        private void SeedLocales(EnvConfigsDbContext context)
        {

            List<Locale> sites = new List<Locale>();

            sites.Add(new Locale()
            {
                SiteId = 1,
                LCID = 1033,
                ShortName = "US",
                Language = "en_us",
                Name = "US",
                Domain = "WeightwWtchers.com",
                OracleName = "WWNA"

            });

            sites.Add(new Locale()
            {
                SiteId = 2,
                Name = "UK",
                ShortName = "UK",
                Domain = "WeightWatchers.co.uk",
                LCID = 2057,
                Language = "en_gb",
                OracleName = "WWEU"

            });

            sites.Add(new Locale()
            {
                SiteId = 3,
                Name = "AU",
                ShortName = "AU",
                Domain = "WeightWatchers.com.au",
                LCID = 3081,
                Language = "en_au",
                OracleName = "WWAU"

            });

            sites.Add(new Locale()
            {
                SiteId = 17,
                Name = "CA",
                ShortName = "CA",
                Domain = "WeightWatchers.ca",
                LCID = 4105,
                Language = "en_ca",
                OracleName = "WWCA"
            });

            sites.Add(new Locale()
            {
                SiteId = 27,
                Name = "FC",
                ShortName = "FC",
                Domain = "fr.WeightWatchers.ca",
                LCID = 3084,
                Language = "fr_ca",
                OracleName = "WWFC"

            });

            sites.Add(new Locale()
            {
                SiteId = 8,
                Name = "DE",
                ShortName = "DE",
                Domain = "weightwatchers.de",
                LCID = 1031,
                Language = "de_de",
                OracleName = "WWDE"

            });

            sites.Add(new Locale()
            {
                SiteId = 9,
                Name = "FR",
                ShortName = "FR",
                Domain = "weightwatchers.fr",
                LCID = 1036,
                Language = "fr_fr",
                OracleName = "WWFR"

            });

            sites.Add(new Locale()
            {
                SiteId = 13,
                Name = "NL",
                ShortName = "NL",
                Domain = "weightwatchers.nl",
                LCID = 1043,
                Language = "nl_nl",
                OracleName = "WWNL"
            });

            sites.Add(new Locale()
            {
                SiteId = 31,
                Name = "NB",
                ShortName = "NB",
                Domain = "weightwatchers.be",
                LCID = 2067,
                Language = "nl_be",
                OracleName = "WWBE_NL"
            });

            sites.Add(new Locale()
            {
                SiteId = 21,
                Name = "FB",
                ShortName = "FB",
                Domain = "fr.weightwatchers.be",
                LCID = 2060,
                Language = "fr_be",
                OracleName = "WWBE_FR"
            });

            sites.Add(new Locale()
            {
                SiteId = 88,
                Name = "China",
                ShortName = "CN",
                Domain = "weightwatchers.com.cn",
                LCID = 2052,
                Language = "zh_cn",
                OracleName = "WWCN"
            });

            sites.Add(new Locale()
            {
                SiteId = 16,
                Name = "SE",
                ShortName = "SE",
                Domain = "viktvaktarna.se",
                LCID = 1053,
                Language = "sv_se",
                OracleName = "WWSE"
            });

            sites.Add(new Locale()
            {
                SiteId = 22,
                Name = "Spain(ES)",
                ShortName = "ES",
                Domain = "entulinea.es",
                LCID = 3082,
                Language = "es_es",
                OracleName = "WWES"
            });
            foreach (var site in sites)
            {
                Locale siteFound = context.Locales.FirstOrDefault(p => p.SiteId == site.SiteId);
                if (siteFound == null)
                {
                    context.Locales.Add(site);
                }
                else
                {
                    site.Id = siteFound.Id;
                    context.Locales.AddOrUpdate<Locale>(site);
                }

            }

            context.SaveChanges();

        }

        private void SeedBuilds(EnvConfigsDbContext context)
        {
            List<Build> builds = new List<Build>();

            builds.Add(new Build() { Name="DEV", Description="DEV" });
            builds.Add(new Build() { Name = "DS", Description = "Devstage" });
            builds.Add(new Build() { Name = "INT", Description = "Integration" });
            builds.Add(new Build() { Name = "QA", Description = "QAT1" });

            foreach (Build b in builds)
            {
                context.Builds.Add(b);
            }
            context.SaveChanges();
            
        }

        private void SeedAppsAndFrameworks(EnvConfigsDbContext context)
        {
            List<WWFramework> list = new List<WWFramework>();

            list.Add(new WWFramework()
            {
                Name = "GC",
                Description = "Global Components",
                WWApps = new List<WWApp>
                  {
                      new WWApp() { Name = "WWCOM", Description="WWCOM"},
                      new WWApp() { Name = "SocNet", Description="Community"},
                      new WWApp() { Name = "RB", Description="Recurring Billing"},
                      new WWApp() { Name = "CATWAT", Description="CATWAT"}
                  }

            });

            list.Add(new WWFramework()
            {
                Name = "FS_GC",
                Description = "FS_GC",
                WWApps = new List<WWApp>
                  {
                      new WWApp() { Name = "CSPortal", Description="CSPortal"}
                  }

            });

            list.Add(new WWFramework()
            {
                Name = "GC_Framework",
                Description = "GC_Framework" ,
                WWApps = new List<WWApp>
                  {
                      new WWApp() { Name = "Food_Service", Description="CSPortal"}
                  }

            });

            foreach (WWFramework f in list)
            {
                context.WWFrameworks.Add(f);
            }

            context.SaveChanges();
        }
    }
}
