using PBDesk.EFRepository;
using System.Collections.Generic;

namespace WW.EnvConfigs.DataModels
{
    public class Build : Entity
    {
        //public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name
        public string Description { get; set; } // Description

        // Reverse navigation
        public virtual ICollection<EnvValue> EnvValues { get; set; }

        public Build()
        {
            EnvValues = new List<EnvValue>();
        }
    }
}
