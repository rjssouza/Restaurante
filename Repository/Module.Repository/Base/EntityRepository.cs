using Module.Dto;
using Module.Repository.Command.Interface.Base;
using Module.Repository.Entity.Base;
using Module.Repository.Interface.Base;
using Module.Repository.Query.Interface.Base;
using System.Collections.Generic;

namespace Module.Repository.Base
{
    public class EntityRepository<TKey, TEntity> : BaseRepository, IEntityRepository<TKey, TEntity>
        where TEntity : BaseEntity<TKey>
    {
        public EntityRepository()
        {
        }

        public IDeleteCommand<TKey, TEntity> DeleteCommand { get; set; }
        public IGetAllByFilterQuery<TKey, TEntity> GetAllByFilterQuery { get; set; }
        public IGetAllQuery<TKey, TEntity> GetAllQuery { get; set; }
        public IGetByIdQuery<TKey, TEntity> GetByIdQuery { get; set; }
        public IGetFirstByFilterQuery<TKey, TEntity> GetFirstByFilterQuery { get; set; }
        public IGetSelectListQuery<TKey, TEntity> GetSelectListQuery { get; set; }
        public IInsertCommand<TKey, TEntity> InsertCommand { get; set; }
        public IUpdateCommand<TKey, TEntity> UpdateCommand { get; set; }

        public int Delete(TEntity entity)
        {
            var result = this.DeleteCommand.Execute(entity);

            return result;
        }

        public IEnumerable<TEntity> GetAll()
        {
            var result = this.GetAllQuery.Execute();

            return result;
        }

        public IEnumerable<TEntity> GetByFilter(object filter)
        {
            var result = this.GetAllByFilterQuery.Execute(filter);

            return result;
        }

        public TEntity GetById(TKey id)
        {
            var result = this.GetByIdQuery.Execute(id);

            return result;
        }

        public TEntity GetFirstByFilter(object filter)
        {
            var result = this.GetFirstByFilterQuery.Execute(filter);

            return result;
        }

        public IEnumerable<GenericSelectDto<TKey>> GetSelectList(object filter = null)
        {
            var result = this.GetSelectListQuery.Execute(filter);

            return result;
        }

        public TKey Insert(TEntity entity)
        {
            var result = this.InsertCommand.Execute(entity);

            return result;
        }

        public int Update(TEntity entity)
        {
            var result = this.UpdateCommand.Execute(entity);

            return result;
        }
    }
}