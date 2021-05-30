using System;
using System.Data;
using Module.Factory.Interface.Base;

namespace Module.Factory.Interface.Connection
{
    public interface IDbConnectionFactory : IBaseFactory
    {
        IDbTransaction Transaction { get; }

        IDbConnection Connection { get; }
    }
}