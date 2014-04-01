using Csla;
using Csla.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.Csla.Lab.BusinessObjects.Customer
{
    [Serializable]
    public class RequestAppoinmentCommand : CommandBase<RequestAppoinmentCommand>
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
