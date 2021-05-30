using System;
using Module.Dto.Base;
using Module.Repository.Entity.Base;
using Module.Repository.Interface.Base;
using Module.Service.Interface.Base;
using Module.Service.Validation.Interface.Base;

namespace Module.Service.Internal
{
    public interface IEntityService<TKey, TEntity, TDto, ITEntityRepository, IEntityValidation> : 
        IEntityService<TKey, TDto>
        where TEntity : BaseEntity<TKey>
        where TDto : BaseDto<TKey>
        where ITEntityRepository : IEntityRepository<TKey, TEntity>
        where IEntityValidation : IEntityValidation<TKey, TEntity>
    {
    }
}