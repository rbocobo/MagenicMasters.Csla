using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.Criteria
{
    public class GetDayScheduleOverrideCriteria : CriteriaBase<GetDayScheduleOverrideCriteria>
    {

        public GetDayScheduleOverrideCriteria(int id, DateTime date)
        {
            LoadProperty(DesignerIdProperty, id);
            LoadProperty(DateProperty, date);
        }

        public static readonly PropertyInfo<int> DesignerIdProperty = RegisterProperty<int>(c => c.DesignerId);
        public int DesignerId
        {
            get { return ReadProperty(DesignerIdProperty); }
            private set { LoadProperty(DesignerIdProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> DateProperty = RegisterProperty<DateTime>(c => c.Date);
        public DateTime Date
        {
            get { return ReadProperty(DateProperty); }
            private set { LoadProperty(DateProperty, value); }
        }
    }
}
