using Module.Dto;
using Module.Dto.Base;
using Module.Repository.Entity.Base;
using Module.Repository.Interface.Base;
using Module.Service.Interface.Base;
using System.Collections.Generic;
using System.Linq;

namespace Module.Service.Base
{
    public class EntityService<TKey, TEntity, TDto, ITEntityRepository> : BaseService, IEntityService<TKey, TDto>
        where TEntity : BaseEntity<TKey>
        where TDto : BaseDto<TKey>
        where ITEntityRepository : IEntityRepository<TKey, TEntity>
    {
        public ITEntityRepository CrudRepository { get; set; }

        public virtual int Delete(TKey id)
        {
            var entity = this.CrudRepository.GetById(id);
            this.BeginTransaction();
            int result = CrudRepository.Delete(entity);
            this.Commit();

            return result;
        }

        public IEnumerable<TDto> GetAll()
        {
            var entityList = this.CrudRepository.GetAll();
            var result = entityList.Select(t => this.ObjectConverter.ConvertTo<TDto>(t));

            return result;
        }

        public IEnumerable<TDto> GetByFilter(object filter)
        {
            var entityList = this.CrudRepository.GetByFilter(filter);
            var result = entityList.Select(t => this.ObjectConverter.ConvertTo<TDto>(t));

            return result;
        }

        public TDto GetById(TKey id)
        {
            var entity = this.CrudRepository.GetById(id);
            var result = this.ObjectConverter.ConvertTo<TDto>(entity);

            return result;
        }

        public TDto GetFirstByFilter(object filter)
        {
            var entity = this.CrudRepository.GetFirstByFilter(filter);
            var result = this.ObjectConverter.ConvertTo<TDto>(entity);

            return result;
        }

        public IEnumerable<GenericSelectDto<TKey>> GetSelectList(object filter = null)
        {
            var result = this.CrudRepository.GetSelectList(filter);

            return result;
        }

        public virtual TKey Insert(TDto dto)
        {
            var entity = this.ObjectConverter.ConvertTo<TEntity>(dto);
            this.BeginTransaction();
            var result = this.CrudRepository.Insert(entity);
            this.Commit();

            return result;
        }

        public virtual int Update(TDto dto)
        {
            var entity = this.ObjectConverter.ConvertTo<TEntity>(dto);
            this.BeginTransaction();
            int result = CrudRepository.Update(entity);
            this.Commit();

            return result;
        }

        protected virtual TKey GenerateId()
        {
            return default;
        }
    }
}