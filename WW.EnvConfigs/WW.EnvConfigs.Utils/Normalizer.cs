using System;
using System.Linq;
using WW.EnvConfigs.DataModels;
using WW.EnvConfigs.DAL;

namespace WW.EnvConfigs.Utils
{
    public class Normalizer
    {
        private static RepoHelper RepoHelper = new RepoHelper();

        public static void RunNormalizer()
        {
            var allKeys = RepoHelper.EnvKeys.GetAll<EnvKey>().ToList<EnvKey>();
            var allLocales = RepoHelper.Locales.GetAll<Locale>().ToList<Locale>();
            var allBuilds = RepoHelper.Builds.GetAll<Build>().ToList<Build>();

            int localesCount = allLocales.Count();
            int buildsCount = allBuilds.Count();
            int keysCount = allKeys.Count();
            int valuesCountPerKey = localesCount * buildsCount;
            int valuesCount = valuesCountPerKey * keysCount;

            foreach(var key in allKeys)
            {
                bool isNormal = true;
                var allValuesPerKey = RepoHelper.EnvValues.Filter<EnvValue>(p => p.EnvKeyId == key.Id).ToList<EnvValue>();
                if (allValuesPerKey.Count() == valuesCountPerKey)
                {
                    foreach(var b in allBuilds)
                    {
                        var x = allValuesPerKey.FindAll(p => p.BuildId == b.Id).Count();
                        if(x != localesCount)
                        {
                            isNormal = false;
                        }
                    }
                     

                }
                else
                {
                    isNormal = false;
                }

                if(!isNormal)
                {
                    Console.WriteLine(key.KeyName);
                }

                

            }

        }
    }
}
