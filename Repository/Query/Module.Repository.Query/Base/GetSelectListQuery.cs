using Autofac.Features.AttributeFilters;
using Dapper;
using Module.Dto;
using Module.Factory.Interface.Connection;
using Module.Repository.Query.Interface.Base;
using System.Collections.Generic;
using System.Linq;

namespace Module.Repository.Query.Base
{
    public class GetSelectListQuery<TKey, TEntity> : BaseQuery, IGetSelectListQuery<TKey, TEntity>
    {
        public GetSelectListQuery([KeyFilter("Query")] IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public IEnumerable<GenericSelectDto<TKey>> Execute(object filter = null)
        {
            var entityList = this._connectionFactory.Connection.GetList<TEntity>(filter, this._connectionFactory.Transaction);
            var selectList = entityList.Select(t => this.ObjectConverter.ConvertTo<GenericSelectDto<TKey>>(t));

            return selectList;
        }
    }
}