using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MagenicMasters.CslaLab.DataAccess.DataContracts;
using MagenicMasters.CslaLab.DataAccess.Models;

namespace MagenicMasters.Csla.Lab.EF.Models.Mapping
{
    public class AppointmentMap : EntityTypeConfiguration<Appointment>
    {
        public AppointmentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("Appointment");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CustomerId).HasColumnName("CustomerId");
            this.Property(t => t.DesignerId).HasColumnName("DesignerId");
            this.Property(t => t.SpecialtyId).HasColumnName("SpecialtyId");
            this.Property(t => t.DateTime).HasColumnName("DateTime");
            this.Property(t => t.Fee).HasColumnName("Fee");
            this.Property(t => t.PartialFee).HasColumnName("PartialFee");
            this.Property(t => t.CancelWindow).HasColumnName("CancelWindow");
            this.Property(t => t.Status).HasColumnName("Status");

            // Relationships
            this.HasRequired(t => t.Customer)
                .WithMany(t => t.Appointments)
                .HasForeignKey(d => d.CustomerId);
            this.HasRequired(t => t.Designer)
                .WithMany(t => t.Appointments)
                .HasForeignKey(d => d.DesignerId);

        }
    }
}
