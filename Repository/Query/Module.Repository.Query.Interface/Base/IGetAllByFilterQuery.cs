using System;
using System.Collections.Generic;
using Module.Repository.Entity.Base;

namespace Module.Repository.Query.Interface.Base
{
    public interface IGetAllByFilterQuery<TKey, TEntity> : IBaseQuery
        where TEntity : BaseEntity<TKey>
    {
        IEnumerable<TEntity> Execute(object filter);
    }
}