using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagenicMasters.CslaLab.DataAccess.Models;

namespace MagenicMasters.CslaLab.Fake
{
    public class FakeDesignerSet : FakeDbSet<Designer>
    {
    
        public FakeDesignerSet()
        {
            this.Add(new Designer() { Id=1, Name="Ned Stark", IsFull =true});
            this.Add(new Designer() { Id=2, Name="Aegon Targaryen", IsFull =false});
            this.Add(new Designer() { Id=3, Name="Roose Bolton", IsFull =true});
            this.Add(new Designer() { Id=4, Name="Tywin Lannister", IsFull =false});
        }
        public override Designer Find(params object[] keyValues)
        {
            return this.SingleOrDefault(_ => _.Id == (int)keyValues.Single());
        }
    }
}
