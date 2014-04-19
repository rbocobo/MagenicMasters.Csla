using Csla;
using MagenicMasters.Csla.Lab.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagenicMasters.CslaLab.DataAccess;

namespace MagenicMasters.CslaLab.Core
{
    [Serializable]
    public abstract class CommandBaseCore<T>
        :CommandBase<T>, ICommandBaseCore
        where T: CommandBaseCore<T>
    {
        [NonSerialized]
        private IMagenicMastersContext context;
        public IMagenicMastersContext Context
        {
            get { return this.context; }
            set { this.context = value; }
        }
    }
}
