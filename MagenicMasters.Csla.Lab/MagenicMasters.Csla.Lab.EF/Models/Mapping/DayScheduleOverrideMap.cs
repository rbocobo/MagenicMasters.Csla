using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MagenicMasters.CslaLab.DataAccess.DataContracts;
using MagenicMasters.CslaLab.DataAccess.Models;

namespace MagenicMasters.Csla.Lab.EF.Models.Mapping
{
    public class DayScheduleOverrideMap : EntityTypeConfiguration<DayScheduleOverride>
    {
        public DayScheduleOverrideMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("DayScheduleOverride");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.WeekScheduleId).HasColumnName("WeekScheduleId");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.EndTime).HasColumnName("EndTime");
            this.Property(t => t.StartTime).HasColumnName("StartTime");

            // Relationships
            this.HasRequired(t => t.WeekSchedule)
                .WithMany(t => t.DayScheduleOverrides)
                .HasForeignKey(d => d.WeekScheduleId);

        }
    }
}
