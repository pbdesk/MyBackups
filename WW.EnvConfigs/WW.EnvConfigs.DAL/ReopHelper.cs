using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WW.EnvConfigs.DAL.Contexts;
using WW.EnvConfigs.DAL;
using WW.EnvConfigs.DataModels;
using PBDesk.EFRepository;

namespace WW.EnvConfigs.DAL
{
    public class RepoHelper : UOWBase
    {
        //private EnvConfigsDbContext context = new EnvConfigsDbContext();

        public RepoHelper()
        {
            context = new EnvConfigsDbContext();
        }

        private Repository<Build> buildRepository;
        private Repository<Locale> localeRepository;
        private Repository<WWFramework> wwFrameworkRepository;
        private Repository<WWApp> wwAppRepository;
        private Repository<EnvKey> envKeyRepository;
        private Repository<EnvValue> envValueRepository;
        
        public dynamic GetReposiotry<T>()
        {
            switch(typeof(T).Name)
            {
                case "Build":
                    {
                        return Builds;
                    }
                case "Locale":
                    {
                        return Locales;
                    }
                case "WWFramework":
                    {
                        return WWFrameworks;
                    }
                case "WWApp":
                    {
                        return WWApps;
                    }
                case "EnvKey":
                    {
                        return EnvKeys;
                    }
                case "EnvValue":
                    {
                        return EnvValues;
                    }
                default:
                    {
                        return null;
                    }
            }

        }
        public Repository<Build> Builds
        {
            get
            {
                if (buildRepository == null)
                {
                    buildRepository = new Repository<Build>(context);
                }
                return buildRepository;
            }

        }
        public Repository<Locale> Locales
        {            
            get
            {
                if (localeRepository == null)
                {
                    localeRepository = new Repository<Locale>(context);
                }
                return localeRepository;
            }

        }
        public Repository<WWFramework> WWFrameworks
        {
            get
            {
                if (wwFrameworkRepository == null)
                {
                    wwFrameworkRepository = new Repository<WWFramework>(context);
                }
                return wwFrameworkRepository;
            }

        }
        public Repository<WWApp> WWApps
        {
            get
            {
                if (wwAppRepository == null)
                {
                    wwAppRepository = new Repository<WWApp>(context);
                }
                return wwAppRepository;
            }

        }
        public Repository<EnvKey> EnvKeys
        {
            get
            {
                if (envKeyRepository == null)
                {
                    envKeyRepository = new Repository<EnvKey>(context);
                }
                return envKeyRepository;
            }

        }
        public Repository<EnvValue> EnvValues
        {
            get
            {
                if (envValueRepository == null)
                {
                    envValueRepository = new Repository<EnvValue>(context);
                }
                return envValueRepository;
            }  
        }



        public EnvKey InsertKeyWithBlankValues(EnvKey t, string lastUpdBy = "")
        {
            var newEntry = EnvKeys.Insert<EnvKey>(t); // context.Set<T>().Add(t);
            if (newEntry != null && newEntry.Id > 0)
            {

                var allBuilds = Builds.GetAll<Build>().ToList();
                var allLocales = Locales.GetAll<Locale>().ToList();
                if (allBuilds != null & allBuilds.Count() > 0 && allLocales != null && allLocales.Count() > 0)
                {
                    foreach (var build in allBuilds)
                    {
                        foreach (var locale in allLocales)
                        {

                            EnvValue v = new EnvValue();
                            v.EnvKeyId = newEntry.Id;
                            v.LocaleId = locale.Id;
                            v.BuildId = build.Id;
                            v.KeyValue = string.Empty;
                            UpdateAuditInfo(v, lastUpdBy);
                            EnvValues.InsertLite<EnvValue>(v);
                        }
                    }
                    SaveChanges();
                }
            }
            return newEntry;
        }

        public Locale InsertLocaleWithBlankValues(Locale l, string lastUpdBy = "")
        {
            var newEntry = Locales.Insert<Locale>(l); // context.Set<T>().Add(t);
            if (newEntry != null && newEntry.Id > 0)
            {

                var allBuilds = Builds.GetAll<Build>().ToList();
                var allKeys = EnvKeys.GetAll<EnvKey>().ToList();
                //var allLocales =  Locales.GetAll<Locale>().ToList();
                if (allBuilds != null & allBuilds.Count() > 0 && allKeys != null && allKeys.Count() > 0)
                {
                    foreach (var build in allBuilds)
                    {
                        foreach (var key in allKeys)
                        {
                            EnvValue v = new EnvValue();
                            v.EnvKeyId = key.Id;
                            v.LocaleId = newEntry.Id;
                            v.BuildId = build.Id;
                            v.KeyValue = string.Empty;
                            UpdateAuditInfo(v, lastUpdBy);
                            EnvValues.InsertLite<EnvValue>(v);
                        }
                    }
                    SaveChanges();
                }
            }
            return newEntry;
        }

        public Build InsertBuildWithBlankValues(Build b, string lastUpdBy = "")
        {
            var newEntry = Builds.Insert<Build>(b); // context.Set<T>().Add(t);
            if (newEntry != null && newEntry.Id > 0)
            {

                //var allBuilds = Builds.GetAll<Build>().ToList();
                var allKeys = EnvKeys.GetAll<EnvKey>().ToList();
                var allLocales = Locales.GetAll<Locale>().ToList();
                if (allLocales != null & allLocales.Count() > 0 && allKeys != null && allKeys.Count() > 0)
                {
                    foreach (var loc in allLocales)
                    {
                        foreach (var key in allKeys)
                        {
                            EnvValue v = new EnvValue();
                            v.EnvKeyId = key.Id;
                            v.LocaleId = loc.Id;
                            v.BuildId = newEntry.Id;
                            v.KeyValue = string.Empty;
                            UpdateAuditInfo(v, lastUpdBy);
                            EnvValues.InsertLite<EnvValue>(v);
                        }
                    }
                    SaveChanges();
                }
            }
            return newEntry;
        }

        

    }
}
