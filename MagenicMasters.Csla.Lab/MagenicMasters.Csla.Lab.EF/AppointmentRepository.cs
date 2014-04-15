using MagenicMasters.CslaLab.EF;
using MagenicMasters.CslaLab.DataAccess.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.EF
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private MagenicMastersCslaContext dataContext = new MagenicMastersCslaContext();

        public CslaLab.DataAccess.DataContracts.IAppointmentData BuildAppointment(int customerId, int specialtyId, bool isFullDesigner, IList<CslaLab.DataAccess.DateTimeRange> dateTimeRanges)
        {
            throw new NotImplementedException();
        }

        public decimal CancelAppointment(int appointmentId)
        {
            throw new NotImplementedException();
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
