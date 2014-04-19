using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MagenicMasters.CslaLab.DataAccess.DataContracts;
using MagenicMasters.CslaLab.DataAccess.Models;

namespace MagenicMasters.Csla.Lab.EF.Models.Mapping
{
    public class DesignerRateMap : EntityTypeConfiguration<DesignerRate>
    {
        public DesignerRateMap()
        {
            this.HasKey(t => t.Id);

            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.ToTable("DesignerRate");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.DesignerId).HasColumnName("DesignerId");
            this.Property(t => t.Rate).HasColumnName("Rate");

            this.HasRequired(t => t.Designer)
                .WithMany(t => t.DesignerRates)
                .HasForeignKey(d => d.DesignerId);
        }
    }
}
