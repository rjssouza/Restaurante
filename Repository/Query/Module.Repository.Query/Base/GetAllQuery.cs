using System;
using System.Collections.Generic;
using Autofac.Features.AttributeFilters;
using Dapper;
using Module.Factory.Interface.Connection;
using Module.Repository.Entity.Base;
using Module.Repository.Query.Interface.Base;

namespace Module.Repository.Query.Base
{
    public class GetAllQuery<TKey, TEntity> : BaseQuery, IGetAllQuery<TKey, TEntity>
        where TEntity : BaseEntity<TKey>
    {
        public GetAllQuery([KeyFilter("Query")] IDbConnectionFactory connectionFactory) 
            : base(connectionFactory)
        {
        }

        public IEnumerable<TEntity> Execute()
        {
            var result = this._connectionFactory.Connection.GetList<TEntity>(this._connectionFactory.Transaction);

            return result;
        }
    }
}