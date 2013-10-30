using System.Collections.Generic;
using WW.EnvConfigs.DataModels;

namespace WW.EnvConfigs.Utils
{
    public class EIParameters
    {
        public string process { get; set; }
        public string file { get; set; }
        public string dir { get; set; }
        public string locale { get; set; }
        public string build { get; set; }
        public string schema { get; set; }
        public string track { get; set; }
        public string db { get; set; }
        public int frameworkId { get; set; }

        
        public List<Locale> LocaleObjs { get; set; }
        public List<Build> BuildObjs { get; set; }
        public WWFramework FrameworkObj { get; set; }
    }
}
