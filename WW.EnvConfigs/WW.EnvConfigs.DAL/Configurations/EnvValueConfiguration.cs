using PBDesk.EFRepository;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WW.EnvConfigs.DAL;
using WW.EnvConfigs.DataModels;

namespace WW.EnvConfigs.DAL.Configurations
{
    public class EnvValueConfiguration : CustomEntityConfiguration<EnvValue>
    {
        public EnvValueConfiguration()  : base()
        {
            ToTable(Constants.TABLE_NM_ENVVALUES);
            //HasKey(x => x.Id);

            //Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.KeyValue).HasColumnName("KeyValue").IsOptional();
            Property(x => x.EnvKeyId).HasColumnName("EnvKeyId").IsRequired();
            Property(x => x.BuildId).HasColumnName("BuildId").IsRequired();
            Property(x => x.LocaleId).HasColumnName("LocaleId").IsRequired();

            // Foreign keys
            HasRequired(a => a.EnvKey).WithMany(b => b.EnvValues).HasForeignKey(c => c.EnvKeyId); 
            HasRequired(a => a.Build).WithMany(b => b.EnvValues).HasForeignKey(c => c.BuildId); 
            HasRequired(a => a.Locale).WithMany(b => b.EnvValues).HasForeignKey(c => c.LocaleId); 
        }
    }
}
