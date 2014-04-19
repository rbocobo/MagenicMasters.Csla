using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagenicMasters.CslaLab.DataAccess.RepositoryContracts;

namespace MagenicMasters.CslaLab.Fake
{
    public class FakeDesignerRepository : IDesignerRepository
    {
        public DataAccess.DataContracts.IDesignerData GetDesigner(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose() { }
    }
}
