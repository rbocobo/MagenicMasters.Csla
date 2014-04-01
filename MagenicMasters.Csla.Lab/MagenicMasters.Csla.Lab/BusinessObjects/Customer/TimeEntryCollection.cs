using System;
using System.Collections.Generic;
using Csla;
using MagenicMasters.CslaLab.Contracts;
using MagenicMasters.CslaLab.Core.Contracts;
using MagenicMasters.CslaLab.CustomAttributes;
using MagenicMasters.CslaLab.Core;

namespace MagenicMasters.CslaLab.Customer
{
    [Serializable]
    public class TimeEntryCollection :
      BusinessListBaseScopeCore<TimeEntryCollection, ITimeEntry>, ITimeEntries
    {

        [InjectedObjectPortal]
        public IChildObjectPortal ChildObjectPortal { get; set; }

        public int IndexOf(IBusinessBaseCore item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, IBusinessBaseCore item)
        {
            throw new NotImplementedException();
        }

        public new IBusinessBaseCore this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(IBusinessBaseCore item)
        {
            throw new NotImplementedException();
        }

        public bool Contains(IBusinessBaseCore item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(IBusinessBaseCore[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(IBusinessBaseCore item)
        {
            throw new NotImplementedException();
        }

        public new IEnumerator<IBusinessBaseCore> GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
