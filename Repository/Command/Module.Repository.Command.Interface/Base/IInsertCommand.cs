using System;
using Module.Repository.Entity.Base;

namespace Module.Repository.Command.Interface.Base
{
    public interface IInsertCommand<TKey, TEntity> : IBaseCommand
        where TEntity : BaseEntity<TKey>
    {
        TKey Execute(TEntity entity);   
    }
}