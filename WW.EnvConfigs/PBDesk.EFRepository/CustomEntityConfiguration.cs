using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBDesk.EFRepository
{
    public class CustomEntityConfiguration<T> : EntityTypeConfiguration<T>     where T:  Entity
    {
        public CustomEntityConfiguration()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.IsActive).HasColumnName("IsActive"); ;
            Property(x => x.LastUpdBy).HasColumnName("LastUpdBy").HasMaxLength(256);
            Property(x => x.LastUpdDate).HasColumnName("LastUpdDate");
        }
    }
}
