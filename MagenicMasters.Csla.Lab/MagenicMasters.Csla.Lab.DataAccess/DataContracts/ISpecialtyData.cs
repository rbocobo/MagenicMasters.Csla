﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.DataAccess.DataContracts
{
    public interface ISpecialtyData
    {
        int Id { get; set; }
        string Name { get; set; }
        //ICollection<IDesignerSpecialtyData> DesignerSpecialties { get; set; }
    }
}
