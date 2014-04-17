using System;
using Csla;
using System.ComponentModel.DataAnnotations;
using MagenicMasters.CslaLab;
using Csla.Rules.CommonRules;
using Csla.Rules;
using MagenicMasters.CslaLab.Common;
using MagenicMasters.CslaLab.Contracts;
using MagenicMasters.CslaLab.Core;
using MagenicMasters.CslaLab.DataAccess.RepositoryContracts;
using MagenicMasters.CslaLab.CustomAttributes;
using MagenicMasters.CslaLab.Criteria;
using MagenicMasters.CslaLab.DataAccess.DataContracts;
using MagenicMasters.CslaLab.Contracts.Designer;
namespace MagenicMasters.CslaLab.Designer
{
    [Serializable]
    public class WorkSchedule : BusinessBaseScopeCore<WorkSchedule>, IWorkSchedule
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            set { SetProperty(IdProperty, value); }
        }           

        public static readonly PropertyInfo<int> DesignerIdProperty = PropertyInfoRegistration.Register<WorkSchedule, int>(_ => _.DesignerId);
        public int DesignerId
        {
            get { return this.GetProperty(WorkSchedule.DesignerIdProperty); }
            set { this.SetProperty(WorkSchedule.DesignerIdProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> StartDateProperty = RegisterProperty<DateTime>(c => c.StartDate);
        [Required]
        public DateTime StartDate
        {
            get { return GetProperty(StartDateProperty); }
            set { SetProperty(StartDateProperty, value); }
        }

        public static readonly PropertyInfo<int> MaxHoursProperty = RegisterProperty<int>(c => c.MaxHours);
        [Required]
        public int MaxHours
        {
            get { return GetProperty(MaxHoursProperty); }
            set { SetProperty(MaxHoursProperty, value); }
        }

        public static readonly PropertyInfo<int> AppointmentIntervalProperty = RegisterProperty<int>(c => c.AppointmentInterval);
        [Required]
        public int AppointmentInterval
        {
            get { return GetProperty(AppointmentIntervalProperty); }
            set { SetProperty(AppointmentIntervalProperty, value); }
        }

        public static readonly PropertyInfo<string> WorkingDaysProperty = RegisterProperty<string>(c => c.WorkingDays);
        [MaxLength(50)]
        [Required]
        public string WorkingDays
        {
            get { return GetProperty(WorkingDaysProperty); }
            set { SetProperty(WorkingDaysProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> StartTimeProperty = RegisterProperty<DateTime>(c => c.StartTime);
        [Required]
        public DateTime StartTime
        {
            get { return GetProperty(StartTimeProperty); }
            set { SetProperty(StartTimeProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> EndTimeProperty = RegisterProperty<DateTime>(c => c.EndTime);
        [Required]
        public DateTime EndTime
        {
            get { return GetProperty(EndTimeProperty); }
            set { SetProperty(EndTimeProperty, value); }
        }

        [Dependency]
        public IScheduleRepository ScheduleRepository { get; set; }

        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            // TODO: add validation rules
            base.AddBusinessRules();

            BusinessRules.AddRule(new MinValue<decimal>(AppointmentIntervalProperty, 0));
            BusinessRules.AddRule(new MinValue<decimal>(MaxHoursProperty, 0));
            //BusinessRules.AddRule(new IsInRole(AuthorizationActions.ReadProperty, WorkingDaysProperty, UserRole.Designers));
            //BusinessRules.AddRule(new IsInRole(AuthorizationActions.WriteProperty, WorkingDaysProperty, UserRole.Designers));
            //BusinessRules.AddRule(new IsInRole(AuthorizationActions.ReadProperty, AppointmentIntervalProperty, UserRole.Designers));
            //BusinessRules.AddRule(new IsInRole(AuthorizationActions.WriteProperty, AppointmentIntervalProperty, UserRole.Designers));
            //BusinessRules.AddRule(new IsInRole(AuthorizationActions.ReadProperty, MaxHoursProperty, UserRole.Designers));
            //BusinessRules.AddRule(new IsInRole(AuthorizationActions.WriteProperty, MaxHoursProperty, UserRole.Designers));

            //BusinessRules.AddRule(new Rule(IdProperty));
        }

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //BusinessRules.AddRule(...);
            
            

           
        }

        #endregion

        #region Data Access

        [RunLocal]
        protected override void DataPortal_Create()
        {
            // TODO: load default values
            // omit this override if you have no defaults to set
            base.DataPortal_Create();
        }

        private void DataPortal_Fetch(GetWeekScheduleCriteria criteria)
        {
            IWeekScheduleData data = this.ScheduleRepository.GetWeekSchedule(criteria.DesignerId, criteria.WeekStartDate);
            using(BypassPropertyChecks)
            {
                Id = data.Id;
                DesignerId = data.DesignerId;
                StartDate = data.StartDate;
                MaxHours = data.MaxHours;
                AppointmentInterval = data.IntervalsInMinutes;
                StartTime = data.StartTime;
                EndTime = data.EndTime;
            }
            
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            IWeekScheduleData data = this.ScheduleRepository.CreateWeekSchedule();
            data.DesignerId = this.DesignerId;
            data.StartDate = this.StartDate;
            data.MaxHours = this.MaxHours;
            data.IntervalsInMinutes = this.AppointmentInterval;
            data.StartTime = this.StartTime;
            data.EndTime = this.EndTime;
            this.ScheduleRepository.AddWeekSchedule(data);
            this.ScheduleRepository.SaveChanges();
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            IWeekScheduleData data = this.ScheduleRepository.CreateWeekSchedule();
            data.Id = this.Id;
            data.DesignerId = this.DesignerId;
            data.StartDate = this.StartDate;
            data.MaxHours = this.MaxHours;
            data.IntervalsInMinutes = this.AppointmentInterval;
            data.StartTime = this.StartTime;
            data.EndTime = this.EndTime;
            this.ScheduleRepository.UpdateWeekSchedule(data);
            this.ScheduleRepository.SaveChanges();
            
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(this.Id);
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(int criteria)
        {
            // TODO: delete values
        }

        #endregion
    }
}
