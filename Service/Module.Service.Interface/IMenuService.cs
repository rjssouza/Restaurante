using System;
using System.Collections.Generic;
using Module.Dto;
using Module.Service.Interface.Base;

namespace Module.Service.Interface
{
    public interface IMenuService : IBaseService
    {
        ClientMenuViewDto GetMenuView();
    }
}