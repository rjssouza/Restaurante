using Module.Dto;
using Module.Dto.ClientOrder;
using Module.Repository.Entity;
using Module.Repository.Interface.Base;
using Module.Service.Base;
using Module.Service.Interface;
using Module.Service.Internal;
using Module.Service.Validation.Interface.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Module.Service
{
    public class ClientOrderService : BaseService, IClientOrderService
    {
        public IEntityService<Guid, ClientTableEntity, ClientTableDto, IEntityRepository<Guid, ClientTableEntity>, IEntityValidation<Guid, ClientTableEntity>> ClientTableService { get; set; }
        public IClientMenuService MenuService { get; set; }
        public IEntityService<Guid, OrderCommandItemEntity, OrderCommandItemDto, IEntityRepository<Guid, OrderCommandItemEntity>, IEntityValidation<Guid, OrderCommandItemEntity>> OrderCommandItemService { get; set; }
        public IEntityService<Guid, OrderCommandEntity, OrderCommandDto, IEntityRepository<Guid, OrderCommandEntity>, IEntityValidation<Guid, OrderCommandEntity>> OrderCommandService { get; set; }

        public int CreateOrder(ClientOrderDto clientOrderDto)
        {
            this.BeginTransaction();
            var orderCommandId = this.CreateOrderCommand(clientOrderDto);
            this.InsertOrderCommandItems(clientOrderDto, orderCommandId);
            this.Commit();
            var orderCommandDto = this.OrderCommandService.GetById(orderCommandId);

            return orderCommandDto.Number;
        }

        public ClientOrderStatusDto GetStatusOrder(int orderNumber)
        {
            var orderCommandDto = this.OrderCommandService.GetFirstByFilter(new { number = orderNumber });
            if (orderCommandDto == null)
                return null;
            var orderCommandItemList = this.OrderCommandItemService.GetByFilter(new { order_command_id = orderCommandDto.Id });

            return new ClientOrderStatusDto()
            {
                Id = orderCommandDto.Id,
                Status = GetClientOrderStatus(orderCommandDto, orderCommandItemList),
                OrderCommandItemList = orderCommandItemList
            };
        }

        private static string GetClientOrderStatus(OrderCommandDto orderCommandDto, IEnumerable<OrderCommandItemDto> orderCommandItemList)
        {
            var orderCommandStatus = orderCommandItemList.All(t => t.Delivered) ? "Entregue" : "Produzindo";
            orderCommandStatus = orderCommandDto.Paid ? "Pago" : orderCommandStatus;

            return orderCommandStatus;
        }

        private Guid CreateOrderCommand(ClientOrderDto clientOrderDto)
        {
            var clientTable = this.ClientTableService.GetById(clientOrderDto.ClientTableId);
            var orderCommandDto = this.ObjectConverter.ConvertTo<OrderCommandDto>(clientOrderDto);
            orderCommandDto.ClientTableId = clientOrderDto.ClientTableId;
            orderCommandDto.Name = $"Comanda para a mesa: {clientTable.Number}";
            orderCommandDto.Price = this.SumOrderPrice(clientOrderDto);
            var result = this.OrderCommandService.Insert(orderCommandDto);

            return result;
        }

        private void InsertOrderCommandItems(ClientOrderDto clientOrderDto, Guid orderCommandId)
        {
            foreach (var item in clientOrderDto.ItemList)
            {
                for (int i = 0; i < item.Quantity; i++)
                {
                    var result = this.ObjectConverter.ConvertTo<OrderCommandItemDto>(item);
                    result.OrderCommandId = orderCommandId;

                    this.OrderCommandItemService.Insert(result);
                }
            }
        }

        private decimal SumOrderPrice(ClientOrderDto clientOrderDto)
        {
            var menuList = this.MenuService.GetMenuList();
            decimal orderPrice = 0;
            foreach (var menuItem in clientOrderDto.ItemList)
            {
                var restaurantMenu = menuList.Where(t => t.Id == menuItem.MenuItemId).FirstOrDefault();

                orderPrice += (restaurantMenu.Price * menuItem.Quantity);
            }

            return orderPrice;
        }
    }
}