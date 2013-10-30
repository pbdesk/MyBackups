﻿using PBDesk.EFRepository;
using System.Collections.Generic;

namespace WW.EnvConfigs.DataModels
{
    public class WWFramework : Entity
    {
        //public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name
        public string Description { get; set; } // Description

        // Reverse navigation
        public virtual ICollection<WWApp> WWApps { get; set; }
        public virtual ICollection<EnvKey> EnvKeys { get; set; }

        public WWFramework()
        {
            WWApps = new List<WWApp>();
            EnvKeys = new List<EnvKey>();
        }
    }
}
