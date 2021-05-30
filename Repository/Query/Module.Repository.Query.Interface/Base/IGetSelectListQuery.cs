using Module.Dto;
using System.Collections.Generic;

namespace Module.Repository.Query.Interface.Base
{
    public interface IGetSelectListQuery<TKey, TEntity> : IBaseQuery
    {
        IEnumerable<GenericSelectDto<TKey>> Execute(object filter = null);
    }
}