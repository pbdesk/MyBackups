using PBDesk.EFRepository;
using System;
using System.Collections.Generic;

namespace WW.EnvConfigs.DataModels
{
    public class EnvKey : Entity
    {
        //public int Id { get; set; } // Id (Primary key)
        public string KeyName { get; set; } // KeyName
        public string KeyDesc { get; set; } // KeyDesc
        public bool IsProtected { get; set; } // IsProtected
        public string CreatedBy { get; set; } // CreatedBy
        public string Protection { get; set; } // Protection
        public DateTime CreateDate { get; set; } // CreateDate
        //public bool IsActive { get; set; } // IsActive
        public int WWFrameworkId { get; set; }

        // Reverse navigation
        public virtual ICollection<EnvValue> EnvValues { get; set; } 
        //public virtual ICollection<WWApp> WWApps { get; set; }
        

        //Foreign Key
        public virtual WWFramework WWFramework { get; set; }

        public EnvKey()
        {
            EnvValues = new List<EnvValue>();
            //WWApps = new List<WWApp>();
        }
    }
}
