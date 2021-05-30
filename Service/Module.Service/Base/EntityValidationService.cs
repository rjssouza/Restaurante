using Module.Dto.Base;
using Module.Repository.Entity.Base;
using Module.Repository.Interface.Base;
using Module.Service.Interface.Base;
using Module.Service.Internal;
using Module.Service.Validation.Interface.Base;

namespace Module.Service.Base
{
    public class EntityValidationService<TKey, TEntity, TDto, ITEntityRepository, IEntityValidation> : 
        EntityService<TKey, TEntity, TDto, ITEntityRepository>, 
        IEntityService<TKey, TDto>,
        IEntityService<TKey, TEntity, TDto, ITEntityRepository, IEntityValidation> 
        where TEntity : BaseEntity<TKey>
        where TDto : BaseDto<TKey>
        where ITEntityRepository : IEntityRepository<TKey, TEntity>
        where IEntityValidation : IEntityValidation<TKey, TEntity>
    {
        public IEntityValidation EntityValidation { get; set; }

        public override int Delete(TKey id)
        {
            var entity = this.CrudRepository.GetById(id);
            this.EntityValidation.ValidateDelete(entity);

            return base.Delete(id);
        }

        public override TKey Insert(TDto dto)
        {
            var entity = this.ObjectConverter.ConvertTo<TEntity>(dto);
            this.EntityValidation.ValidateInsert(entity);

            return base.Insert(dto);
        }

        public override int Update(TDto dto)
        {
            var entity = this.ObjectConverter.ConvertTo<TEntity>(dto);
            this.EntityValidation.ValidateUpdate(entity);

            return base.Update(dto);
        }
    }
}