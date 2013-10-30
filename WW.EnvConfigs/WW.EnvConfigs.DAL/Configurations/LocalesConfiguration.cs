using PBDesk.EFRepository;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WW.EnvConfigs.DAL;
using WW.EnvConfigs.DataModels;

namespace WW.EnvConfigs.DAL.Configurations
{
    public class LocalesConfiguration : CustomEntityConfiguration<Locale>
    {
        public LocalesConfiguration()  : base()
        {
            ToTable(Constants.TABLE_NM_LOCALES);
            //HasKey(x => x.Id);
            //Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.SiteId).HasColumnName("SiteId").IsRequired();
            Property(x => x.LCID).HasColumnName("LCID").IsOptional();
            Property(x => x.ShortName).HasColumnName("ShortName").IsRequired().HasMaxLength(2);
            Property(x => x.Name).HasColumnName("Name").IsOptional();
            Property(x => x.Domain).HasColumnName("Domain").IsOptional();
            Property(x => x.Language).HasColumnName("Language").IsOptional().HasMaxLength(5);
            Property(x => x.OracleName).HasColumnName("OracleName").IsOptional();

            
        }
    }
}
