using System;
using Autofac.Features.AttributeFilters;
using Dapper;
using Module.Factory.Interface.Connection;
using Module.Repository.Command.Interface.Base;
using Module.Repository.Entity.Base;

namespace Module.Repository.Command.Base
{
    public class InsertCommand<TKey, TEntity> : BaseCommand<TKey, TKey, TEntity>, IInsertCommand<TKey, TEntity>
        where TEntity : BaseEntity<TKey>
    {
        public InsertCommand([KeyFilter("Command")]IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public override TKey Execute(TEntity entity)
        {
            var result = this._connectionFactory.Connection.Insert<TKey, TEntity>(entity, this._connectionFactory.Transaction);

            return result;
        }
    }
}