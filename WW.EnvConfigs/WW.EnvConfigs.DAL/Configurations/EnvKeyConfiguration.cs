using PBDesk.EFRepository;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WW.EnvConfigs.DAL;
using WW.EnvConfigs.DataModels;


namespace WW.EnvConfigs.DAL.Configurations
{
    class EnvKeyConfiguration : CustomEntityConfiguration<EnvKey>
    {
        public EnvKeyConfiguration()    : base()
        {
            ToTable(Constants.TABLE_NM_ENVKEYS);
            //HasKey(x => x.Id);

            //Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.KeyName).HasColumnName("KeyName").IsRequired().HasMaxLength(500);
            Property(x => x.KeyDesc).HasColumnName("KeyDesc").IsOptional();
            Property(x => x.IsProtected).HasColumnName("IsProtected").IsRequired();
            Property(x => x.CreatedBy).HasColumnName("CreatedBy").IsOptional();
            Property(x => x.Protection).HasColumnName("Protection").IsOptional();
            Property(x => x.CreateDate).HasColumnName("CreateDate").IsRequired();
            //Property(x => x.IsActive).HasColumnName("IsActive").IsRequired();
            Property(x => x.WWFrameworkId).HasColumnName("WWFrameworkId").IsRequired();

            // Foreign keys
            HasRequired(a => a.WWFramework).WithMany(b => b.EnvKeys).HasForeignKey(c => c.WWFrameworkId); 
        }

    }
}
