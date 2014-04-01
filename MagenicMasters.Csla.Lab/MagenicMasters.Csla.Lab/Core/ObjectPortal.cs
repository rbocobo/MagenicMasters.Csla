using Csla;
using Csla.Serialization.Mobile;
using MagenicMasters.Csla.Lab.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.Csla.Lab.Core
{
    public sealed class ObjectPortal<T> : IObjectPortal<T>
        where T : class, IMobileObject
    {
        public void BeginCreate(object criteria, object userState)
        {
            throw new NotImplementedException();
        }

        public void BeginCreate(object criteria)
        {
            throw new NotImplementedException();
        }

        public void BeginCreate()
        {
            throw new NotImplementedException();
        }

        public void BeginDelete(object criteria, object userState)
        {
            throw new NotImplementedException();
        }

        public void BeginDelete(object criteria)
        {
            throw new NotImplementedException();
        }

        public void BeginExecute(T command, object userState)
        {
            throw new NotImplementedException();
        }

        public void BeginExecute(T command)
        {
            throw new NotImplementedException();
        }

        public void BeginFetch(object criteria, object userState)
        {
            throw new NotImplementedException();
        }

        public void BeginFetch(object criteria)
        {
            throw new NotImplementedException();
        }

        public void BeginFetch()
        {
            throw new NotImplementedException();
        }

        public void BeginUpdate(T obj, object userState)
        {
            throw new NotImplementedException();
        }

        public void BeginUpdate(T obj)
        {
            throw new NotImplementedException();
        }

        public T Create()
        {
            return DataPortal.Create<T>();
        }

        public T Create(object criteria)
        {
            return DataPortal.Create<T>(criteria);
        }

        public Task<T> CreateAsync(object criteria)
        {
            return DataPortal.CreateAsync<T>(criteria);
        }

        public Task<T> CreateAsync()
        {
            return DataPortal.CreateAsync<T>();
        }

        public event EventHandler<global::Csla.DataPortalResult<T>> CreateCompleted;

        public void Delete(object criteria)
        {
            DataPortal.Delete<T>(criteria);
        }

        public Task DeleteAsync(object criteria)
        {
            return DataPortal.DeleteAsync<T>(criteria);
        }

        public event EventHandler<global::Csla.DataPortalResult<T>> DeleteCompleted;

        public T Execute(T obj)
        {
            return DataPortal.Execute<T>(obj);
        }

        public Task<T> ExecuteAsync(T command)
        {
            return DataPortal.ExecuteAsync<T>(command);
        }

        public event EventHandler<global::Csla.DataPortalResult<T>> ExecuteCompleted;

        public T Fetch()
        {
            return DataPortal.Fetch<T>();
        }

        public T Fetch(object criteria)
        {
            return DataPortal.Fetch<T>(criteria);
        }

        public Task<T> FetchAsync(object criteria)
        {
            return DataPortal.FetchAsync<T>(criteria);
        }

        public  Task<T> FetchAsync()
        {
            return DataPortal.FetchAsync<T>();
        }

        public event EventHandler<global::Csla.DataPortalResult<T>> FetchCompleted;

        public global::Csla.Core.ContextDictionary GlobalContext
        {
            get { throw new NotImplementedException(); }
        }

        public T Update(T obj)
        {
            return DataPortal.Update<T>(obj);
        }

        public Task<T> UpdateAsync(T obj)
        {
            return DataPortal.UpdateAsync(obj);
        }

        public event EventHandler<global::Csla.DataPortalResult<T>> UpdateCompleted;
    }
}
