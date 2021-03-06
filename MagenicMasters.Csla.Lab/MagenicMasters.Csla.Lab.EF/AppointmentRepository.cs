﻿using MagenicMasters.CslaLab.EF;
using MagenicMasters.CslaLab.DataAccess.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagenicMasters.CslaLab.DataAccess.DataContracts;
using MagenicMasters.Csla.Lab.EF.Models;
using MagenicMasters.Csla.Lab.DataAccess;
using MagenicMasters.CslaLab.DataAccess;

namespace MagenicMasters.CslaLab.EF
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private IMagenicMastersContext context = MMContext.context;

        public CslaLab.DataAccess.DataContracts.IAppointmentData BuildAppointment(int customerId, int specialtyId, bool isFullDesigner, IList<CslaLab.DataAccess.DateTimeRange> dateTimeRanges)
        {
            foreach (var item in dateTimeRanges)
	        {
                

                var availableSchedule =  context.WeekSchedules.Where
                    (w =>
                        item.StartDateTime >= w.StartDate // &&
                        //item.EndDateTime <= w.StartDate &&
                        //w.Designer.DesignerSpecialties.Any(d => d.SpecialtyId == specialtyId) &&
                        //!w.Designer.Appointments.Any(a => a.DateTime >= item.StartDateTime && a.DateTime <= item.EndDateTime)
                     )
                     .Select(_ => new 
                     {
                         CustomerId = customerId,
                         DesignerId = _.Designer.Id,
                         CancelWindow = context.Cancellations.FirstOrDefault().Window,
                         Fee = _.Designer.DesignerRates.FirstOrDefault().Rate,
                         DateTime = item.StartDateTime,
                         PartialFee = 50,
                         Status = 1
                     })
                     .FirstOrDefault();


                        

                if (availableSchedule != null)
                {
                    var appointment = context.Appointments.Create();
                    appointment.CustomerId = availableSchedule.CustomerId;
                    appointment.DesignerId = availableSchedule.DesignerId;
                    appointment.CancelWindow = availableSchedule.CancelWindow;
                    appointment.Fee = availableSchedule.Fee;
                    appointment.PartialFee = availableSchedule.PartialFee;
                    appointment.Status = availableSchedule.Status;
                    appointment.DateTime = availableSchedule.DateTime;
                    
                    context.Appointments.Add(appointment);
                    context.SaveChanges();
                    return appointment;
                    break;
                }

               
	        }
             return null;
        }

        public decimal CancelAppointment(int appointmentId)
        {
            var cancelSettings = context.Cancellations.Select(_ => new { _.Fee, _.Window}).FirstOrDefault();
            var appointment = context.Appointments.Find(appointmentId);
            appointment.Status = 0;
            int i= context.SaveChanges();
            if (i > 0)
            {
                int dayDiff = (appointment.DateTime - DateTime.Now).Days;
                if (dayDiff <= appointment.CancelWindow)
                    return cancelSettings.Fee;
                else
                    return cancelSettings.Fee / 2;

            }
            return 0;

        }

        public IEnumerable<CslaLab.DataAccess.DataContracts.IAppointmentData> GetDesignerActiveAppointments(int designerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CslaLab.DataAccess.DataContracts.IAppointmentData> GetCustomerActiveAppointments(int customerId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }
    }
}
