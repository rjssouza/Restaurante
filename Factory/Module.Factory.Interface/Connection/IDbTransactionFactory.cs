using System;
using System.Data;
using Module.Factory.Interface.Base;

namespace Module.Factory.Interface.Connection
{
    public interface IDbTransactionFactory : IBaseFactory
    {
        void BeginTransaction();

        void Commit();

        void Rollback();
    }
}