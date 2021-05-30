using Module.Dto;
using Module.Repository.Entity.Base;
using System.Collections.Generic;

namespace Module.Repository.Interface.Base
{
    public interface IEntityRepository<TKey, TEntity> : IBaseRepository
        where TEntity : BaseEntity<TKey>
    {
        int Delete(TEntity entity);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetByFilter(object filter);

        TEntity GetById(TKey id);

        TEntity GetFirstByFilter(object filter);

        IEnumerable<GenericSelectDto<TKey>> GetSelectList(object filter = null);

        TKey Insert(TEntity entity);

        int Update(TEntity entity);
    }
}