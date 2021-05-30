using System;
using Autofac.Features.AttributeFilters;
using Dapper;
using Module.Factory.Interface.Connection;
using Module.Repository.Command.Interface.Base;
using Module.Repository.Entity.Base;

namespace Module.Repository.Command.Base
{
    public class UpdateCommand<TKey, TEntity> : BaseCommand<int, TKey, TEntity>, IUpdateCommand<TKey, TEntity>
        where TEntity : BaseEntity<TKey>
    {
        public UpdateCommand([KeyFilter("Command")]IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public override int Execute(TEntity entity)
        {
            var result = this._connectionFactory.Connection.Update<TEntity>(entity, this._connectionFactory.Transaction);

            return result;
        }
    }
}