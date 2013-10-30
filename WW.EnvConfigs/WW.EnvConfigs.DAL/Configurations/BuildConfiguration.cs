using PBDesk.EFRepository;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WW.EnvConfigs.DAL;
using WW.EnvConfigs.DataModels;


namespace WW.EnvConfigs.DAL.Configurations
{
    public class BuildConfiguration : CustomEntityConfiguration<Build>
    {
        public BuildConfiguration() : base()
        {
            ToTable(Constants.TABLE_NM_BUILDS);
            //HasKey(x => x.Id);            
            //Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(10);
            Property(x => x.Description).HasColumnName("Description").IsOptional();
        }
    }
}
