using Csla;
using MagenicMasters.CslaLab.DataAccess.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.Contracts
{
    public interface IAppointmentResultView : IReadOnlyBase
    {
        DateTime StartDateTime { get;  }
        DateTime EndDateTime { get;  }

        [MaxLength(200)]
        string DesignerName { get;  }

        decimal Fee { get; }

        decimal PartialFee { get;  }

        IDesignerRepository DesignerRepository { get; set; }
    }
}
