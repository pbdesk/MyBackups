
using PBDesk.EFRepository;
namespace WW.EnvConfigs.DataModels
{
    public class EnvValue : Entity
    {
        //public int Id { get; set; } 
        public string KeyValue { get; set; }
        public int EnvKeyId { get; set; } 
        public int BuildId { get; set; }  
        public int LocaleId { get; set; }

        // Foreign keys
        public virtual EnvKey EnvKey { get; set; } 
        public virtual Build Build { get; set; } 
        public virtual Locale Locale { get; set; } 
    }
}
