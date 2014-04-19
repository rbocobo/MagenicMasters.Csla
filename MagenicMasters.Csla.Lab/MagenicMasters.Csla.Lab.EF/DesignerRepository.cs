using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagenicMasters.Csla.Lab.DataAccess;
using MagenicMasters.CslaLab.DataAccess;
using MagenicMasters.CslaLab.DataAccess.RepositoryContracts;
using MagenicMasters.CslaLab.EF;
namespace MagenicMasters.CslaLab.EF
{
    public class DesignerRepository : IDesignerRepository
    {
        private IMagenicMastersContext context = MMContext.context;
        public CslaLab.DataAccess.DataContracts.IDesignerData GetDesigner(int id)
        {
            return context.Designers.Find(id);
        }

        public void Dispose()
        {
            
        }
    }
}
