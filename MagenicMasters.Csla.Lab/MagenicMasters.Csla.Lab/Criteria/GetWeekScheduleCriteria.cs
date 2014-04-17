using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.Criteria
{
    public class GetWeekScheduleCriteria : CriteriaBase<GetWeekScheduleCriteria>
    {

        public GetWeekScheduleCriteria(int designerId, DateTime weekStartDate)
        {
            LoadProperty(DesignerIdProperty, designerId);
            LoadProperty(WeekStartDateProperty, weekStartDate);
        }

        public static readonly PropertyInfo<int> DesignerIdProperty = RegisterProperty<int>(c => c.DesignerId);
        public int DesignerId
        {
            get { return ReadProperty(DesignerIdProperty); }
            private set { LoadProperty(DesignerIdProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> WeekStartDateProperty = RegisterProperty<DateTime>(c => c.WeekStartDate);
        public DateTime WeekStartDate
        {
            get { return ReadProperty(WeekStartDateProperty); }
            private set { LoadProperty(WeekStartDateProperty, value); }
        }
    }
}
