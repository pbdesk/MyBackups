using PBDesk.EFRepository;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WW.EnvConfigs.DAL;
using WW.EnvConfigs.DataModels;


namespace WW.EnvConfigs.DAL.Configurations
{
    public class WWFrameworksConfiguration : CustomEntityConfiguration<WWFramework>
    {
        public WWFrameworksConfiguration() : base()
        {
            ToTable(Constants.TABLE_NM_WWFRAMEWORKS);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(50);
            Property(x => x.Description).HasColumnName("Description").IsOptional();

            
        }
    }
}
