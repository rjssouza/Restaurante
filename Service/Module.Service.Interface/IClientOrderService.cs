using System;
using Module.Dto;
using Module.Dto.ClientOrder;
using Module.Service.Interface.Base;

namespace Module.Service.Interface
{
    public interface IClientOrderService : IBaseService
    {
        int CreateOrder(ClientOrderDto clientOrderDto);

        ClientOrderStatusDto GetStatusOrder(int orderNumber);
    }
}