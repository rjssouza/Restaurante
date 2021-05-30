using System;
using Module.Repository.Entity.Base;

namespace Module.Repository.Command.Interface.Base
{
    public interface IUpdateCommand<TKey, TEntity> : IBaseCommand
        where TEntity : BaseEntity<TKey>
    {
        int Execute(TEntity entity);
    }
}