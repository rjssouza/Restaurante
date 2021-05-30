using System;
using Module.Repository.Entity.Base;

namespace Module.Repository.Query.Interface.Base
{
    public interface IGetByIdQuery<TKey, TEntity> : IBaseQuery
        where TEntity : BaseEntity<TKey>
    {
        TEntity Execute(TKey id);
    }
}