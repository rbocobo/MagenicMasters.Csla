using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MagenicMasters.CslaLab.DataAccess.DataContracts;
using MagenicMasters.CslaLab.DataAccess.Models;

namespace MagenicMasters.Csla.Lab.EF.Models.Mapping
{
    public class CancellationMap : EntityTypeConfiguration<Cancellation>
    {
        public CancellationMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("Cancellation");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Window).HasColumnName("Window");
            this.Property(t => t.Fee).HasColumnName("Fee");
        }
    }
}
