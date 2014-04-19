using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MagenicMasters.CslaLab.DataAccess.DataContracts;
using MagenicMasters.CslaLab.DataAccess.Models;

namespace MagenicMasters.Csla.Lab.EF.Models.Mapping
{
    public class DesignerMap : EntityTypeConfiguration<Designer>
    {
        public DesignerMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Designer");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IsFull).HasColumnName("IsFull");
        }
    }
}
