using System;
using Module.Repository.Entity.Base;

namespace Module.Service.Validation.Interface.Base
{
    public interface IEntityValidation<TKey, TEntity> : IBaseValidation
        where TEntity : BaseEntity<TKey>
    {
        void ValidateInsert(TEntity entity);
        void ValidateUpdate(TEntity entity);
        void ValidateDelete(TEntity entity);
    }
}