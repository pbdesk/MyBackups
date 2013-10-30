using PBDesk.EFRepository;
using System.Collections.Generic;


namespace WW.EnvConfigs.DataModels
{

    public class Locale : Entity
    {
        //public int Id { get; set; }

        public int SiteId { get; set; }
        public int LCID { get; set; }

        public string ShortName { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }
        public string Language { get; set; }
        public string OracleName { get; set; }

        // Reverse navigation
        public virtual ICollection<EnvValue> EnvValues { get; set; }

        public Locale()
        {
            EnvValues = new List<EnvValue>();
        }
    }
}
