using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Features.AttributeFilters;
using Dapper;
using Module.Factory.Interface.Connection;
using Module.Repository.Entity.Base;
using Module.Repository.Query.Interface.Base;

namespace Module.Repository.Query.Base
{
    public class GetFirstByFilterQuery<TKey, TEntity> : BaseQuery, IGetFirstByFilterQuery<TKey, TEntity>
        where TEntity : BaseEntity<TKey>
    {
        public GetFirstByFilterQuery([KeyFilter("Query")] IDbConnectionFactory connectionFactory) 
            : base(connectionFactory)
        {
        }

        public TEntity Execute(object filter)
        {
            var resultList = this._connectionFactory.Connection.GetList<TEntity>(filter, this._connectionFactory.Transaction);
            var result = resultList.FirstOrDefault();

            return result;
        }
    }
}