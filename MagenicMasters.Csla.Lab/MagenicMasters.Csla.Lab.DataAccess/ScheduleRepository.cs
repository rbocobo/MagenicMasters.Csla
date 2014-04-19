using MagenicMasters.CslaLab.DataAccess.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagenicMasters.CslaLab.DataAccess.Models;
//using System.Data.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using MagenicMasters.CslaLab.DataAccess.Models;
using MagenicMasters.CslaLab.DataAccess;
using MagenicMasters.Csla.Lab.DataAccess;
namespace MagenicMasters.CslaLab.DataAccess
{
   public  class ScheduleRepository : IScheduleRepository
    {
       private IMagenicMastersContext context = MMContext.context;

        public DataAccess.DataContracts.IWeekScheduleData CreateWeekSchedule()
        {
            return context.WeekSchedules.Create();
            
        }

        public void AddWeekSchedule(DataAccess.DataContracts.IWeekScheduleData weekSchedule)
        {
            context.WeekSchedules.Add((WeekSchedule)weekSchedule);
            //context.WeekSchedules.Attach((WeekSchedule)weekSchedule);
            //((IObjectContextAdapter)context).ObjectContext.ObjectStateManager.ChangeObjectState((WeekSchedule)weekSchedule, System.Data.Entity.EntityState.Added);
        }

        public DataAccess.DataContracts.IWeekScheduleData GetWeekSchedule(int designerId, DateTime weekStartDate)
        {
            return context.WeekSchedules.Where(w=> w.DesignerId == designerId && w.StartDate == weekStartDate).FirstOrDefault();
        }

        public void UpdateWeekSchedule(DataAccess.DataContracts.IWeekScheduleData weekSchedule)
        {
            var item = context.WeekSchedules.Find(weekSchedule.Id);
            if (item != null)
            {
                item.DesignerId = weekSchedule.DesignerId;
                item.IncludeHolidays = weekSchedule.IncludeHolidays;
                item.IntervalsInMinutes = weekSchedule.IntervalsInMinutes;
                item.MaxHours = weekSchedule.MaxHours;
                item.StartDate = weekSchedule.StartDate;
                item.StartTime  = weekSchedule.StartTime;
                item.EndTime = weekSchedule.EndTime;

            }
        }

        public DataAccess.DataContracts.IDayScheduleOverrideData CreateDayScheduleOverride()
        {
            return context.DayScheduleOverrides.Create();
        }

        public void AddDayScheduleOverride(DataAccess.DataContracts.IDayScheduleOverrideData daySchedule)
        {
            context.DayScheduleOverrides.Add((DayScheduleOverride)daySchedule);
            
        }

        public DataAccess.DataContracts.IDayScheduleOverrideData GetDayScheduleOverride(int designerId, DateTime date)
        {
            return context.DayScheduleOverrides.Where(d => d.WeekSchedule.DesignerId == designerId && d.Date == date).FirstOrDefault();
        }

        public void UpdateDayScheduleOverride(DataAccess.DataContracts.IDayScheduleOverrideData daySchedule)
        {
            var item = context.DayScheduleOverrides.Find(daySchedule.Id);
            item.Date = daySchedule.Date;
            item.EndTime = daySchedule.EndTime;
            item.StartTime = daySchedule.StartTime;
            item.WeekScheduleId = daySchedule.WeekScheduleId;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
        public void Dispose()
        {
            
        }
        
    }
}
