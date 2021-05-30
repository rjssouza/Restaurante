using System;
using System.Collections.Generic;
using Module.Repository.Entity.Base;

namespace Module.Repository.Query.Interface.Base
{
    public interface IGetFirstByFilterQuery<TKey, TEntity> : IBaseQuery
        where TEntity : BaseEntity<TKey>
    {
        TEntity Execute(object filter);
    }
}