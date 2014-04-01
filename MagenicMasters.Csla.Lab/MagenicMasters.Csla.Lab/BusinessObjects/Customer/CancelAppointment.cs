using System;
using Csla;

namespace MagenicMasters.CslaLab.Customer
{
    [Serializable]
    public class CancelAppointment : CommandBase<CancelAppointment>
    {
        #region Authorization Methods

        public static bool CanExecuteCommand()
        {
            // TODO: customize to check user role
            //return Csla.ApplicationContext.User.IsInRole("Role");
            return true;
        }

        #endregion


       
    }
}
