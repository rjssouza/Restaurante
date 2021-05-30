using Module.Dto;
using Module.Dto.Base;
using System.Collections.Generic;

namespace Module.Service.Interface.Base
{
    public interface IEntityService<TKey, TDto> : IBaseService
        where TDto : BaseDto<TKey>
    {
        int Delete(TKey id);

        IEnumerable<TDto> GetAll();

        IEnumerable<TDto> GetByFilter(object filter);

        TDto GetById(TKey id);

        TDto GetFirstByFilter(object filter);

        IEnumerable<GenericSelectDto<TKey>> GetSelectList(object filter = null);

        TKey Insert(TDto dto);

        int Update(TDto dto);
    }
}