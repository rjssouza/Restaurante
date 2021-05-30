using System;
using Autofac.Features.AttributeFilters;
using Dapper;
using Module.Factory.Interface.Connection;
using Module.Repository.Entity.Base;
using Module.Repository.Query.Interface.Base;

namespace Module.Repository.Query.Base
{
    public class GetByIdQuery<TKey, TEntity> : BaseQuery, IGetByIdQuery<TKey, TEntity>
        where TEntity : BaseEntity<TKey>
    {
        public GetByIdQuery([KeyFilter("Query")] IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public TEntity Execute(TKey id)
        {
            var result = this._connectionFactory.Connection.Get<TEntity>(id, this._connectionFactory.Transaction);

            return result;
        }
    }
}