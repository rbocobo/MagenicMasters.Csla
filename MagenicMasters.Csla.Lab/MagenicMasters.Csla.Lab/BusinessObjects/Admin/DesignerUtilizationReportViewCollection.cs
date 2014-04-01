using System;
using System.Collections.Generic;
using Csla;

namespace MagenicMasters.CslaLab.Admin
{
    [Serializable]
    public class DesignerUtilizationReportViewCollection :
      ReadOnlyListBase<DesignerUtilizationReportViewCollection, DesignerUtilizationReportView>
    {
        #region Authorization Rules

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //AuthorizationRules.AllowGet(typeof(DesignerUtilizationReportViewCollection), "Role");
        }

        #endregion

        #region Factory Methods

        internal static DesignerUtilizationReportViewCollection GetReadOnlyChildList(object childData)
        {
            return DataPortal.FetchChild<DesignerUtilizationReportViewCollection>(childData);
        }

        private DesignerUtilizationReportViewCollection()
        { /* require use of factory methods */ }

        #endregion
        #region Data Access

        private void Child_Fetch(object childData)
        {
            RaiseListChangedEvents = false;
            IsReadOnly = false;
            // TODO: load values
            foreach (var child in (List<object>)childData)
                Add(DesignerUtilizationReportView.GetReadOnlyChild(child));
            IsReadOnly = true;
            RaiseListChangedEvents = true;
        }

        #endregion
    }
}
