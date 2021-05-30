using System;
using Module.Repository.Entity.Base;

namespace Module.Repository.Command.Interface.Base
{
    public interface IDeleteCommand<TKey, TEntity> : IBaseCommand
        where TEntity : BaseEntity<TKey>
    {
        int Execute(TEntity entity);
    }
}