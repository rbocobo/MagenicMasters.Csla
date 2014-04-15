using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MagenicMasters.Csla.Lab.EF.Models.Mapping
{
    public class WeekScheduleMap : EntityTypeConfiguration<WeekSchedule>
    {
        public WeekScheduleMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("WeekSchedule");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.DesignerId).HasColumnName("DesignerId");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.StartTime).HasColumnName("StartTime");
            this.Property(t => t.EndTime).HasColumnName("EndTime");
            this.Property(t => t.IntervalsInMinutes).HasColumnName("IntervalsInMinutes");
            this.Property(t => t.MaxHours).HasColumnName("MaxHours");
            this.Property(t => t.IncludeHolidays).HasColumnName("IncludeHolidays");

            // Relationships
            this.HasRequired(t => t.Designer)
                .WithMany(t => t.WeekSchedules)
                .HasForeignKey(d => d.DesignerId);

        }
    }
}
