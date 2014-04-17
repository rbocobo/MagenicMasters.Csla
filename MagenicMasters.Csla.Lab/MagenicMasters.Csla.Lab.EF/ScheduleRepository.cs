using MagenicMasters.CslaLab.DataAccess.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.EF
{
   public  class ScheduleRepository : IScheduleRepository
    {
       private MagenicMastersCslaContext context = new MagenicMastersCslaContext();
        public DataAccess.DataContracts.IWeekScheduleData CreateWeekSchedule()
        {
            return context.WeekSchedules.Create();
        }

        public void AddWeekSchedule(DataAccess.DataContracts.IWeekScheduleData weekSchedule)
        {
            throw new NotImplementedException();
        }

        public DataAccess.DataContracts.IWeekScheduleData GetWeekSchedule(int designerId, DateTime weekStartDate)
        {
            throw new NotImplementedException();
        }

        public void UpdateWeekSchedule(DataAccess.DataContracts.IWeekScheduleData weekSchedule)
        {
            throw new NotImplementedException();
        }

        public DataAccess.DataContracts.IDayScheduleOverrideData CreateDayScheduleOverride()
        {
            throw new NotImplementedException();
        }

        public void AddDayScheduleOverride(DataAccess.DataContracts.IDayScheduleOverrideData daySchedule)
        {
            throw new NotImplementedException();
        }

        public DataAccess.DataContracts.IDayScheduleOverrideData GetDayScheduleOverride(int designerId, DateTime date)
        {
            throw new NotImplementedException();
        }

        public void UpdateDayScheduleOverride(DataAccess.DataContracts.IDayScheduleOverrideData daySchedule)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
