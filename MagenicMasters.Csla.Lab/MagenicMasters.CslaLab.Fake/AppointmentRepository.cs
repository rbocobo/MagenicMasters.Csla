using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagenicMasters.CslaLab.DataAccess.RepositoryContracts;

namespace MagenicMasters.CslaLab.Fake
{
    public class AppointmentRepository : IAppointmentRepository
    {
        public DataAccess.DataContracts.IAppointmentData BuildAppointment(int customerId, int specialtyId, bool isFullDesigner, IList<DataAccess.DateTimeRange> dateTimeRanges)
        {
            throw new NotImplementedException();
        }

        public decimal CancelAppointment(int appointmentId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DataAccess.DataContracts.IAppointmentData> GetDesignerActiveAppointments(int designerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DataAccess.DataContracts.IAppointmentData> GetCustomerActiveAppointments(int customerId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
