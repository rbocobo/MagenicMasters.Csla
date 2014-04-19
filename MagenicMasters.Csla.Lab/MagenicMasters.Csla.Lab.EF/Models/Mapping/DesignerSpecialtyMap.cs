using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MagenicMasters.CslaLab.DataAccess.DataContracts;
using MagenicMasters.CslaLab.DataAccess.Models;

namespace MagenicMasters.Csla.Lab.EF.Models.Mapping
{
    public class DesignerSpecialtyMap : EntityTypeConfiguration<DesignerSpecialty>
    {
        public DesignerSpecialtyMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("DesignerSpecialty");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.DesignerId).HasColumnName("DesignerId");
            this.Property(t => t.SpecialtyId).HasColumnName("SpecialtyId");

            // Relationships
            this.HasRequired(t => t.Designer)
                .WithMany(t => t.DesignerSpecialties)
                .HasForeignKey(d => d.DesignerId);
            this.HasRequired(t => t.Specialty)
                .WithMany(t => t.DesignerSpecialties)
                .HasForeignKey(d => d.SpecialtyId);

        }
    }
}
