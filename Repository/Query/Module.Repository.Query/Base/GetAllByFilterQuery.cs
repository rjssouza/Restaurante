using System;
using System.Collections.Generic;
using Autofac.Features.AttributeFilters;
using Module.Factory.Interface.Connection;
using Dapper;
using Module.Repository.Query.Interface.Base;
using Module.Repository.Entity.Base;

namespace Module.Repository.Query.Base
{
    public class GetAllByFilterQuery<TKey, TEntity> : BaseQuery, IGetAllByFilterQuery<TKey, TEntity>
        where TEntity : BaseEntity<TKey>
    {
        public GetAllByFilterQuery([KeyFilter("Query")] IDbConnectionFactory connectionFactory) 
            : base(connectionFactory)
        {
        }

        public IEnumerable<TEntity> Execute(object filter)
        {
            var result = this._connectionFactory.Connection.GetList<TEntity>(filter, this._connectionFactory.Transaction);

            return result;
        }
    }
}