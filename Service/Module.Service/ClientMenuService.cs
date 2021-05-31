using Module.Dto;
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
    public class ClientMenuService : BaseService, IClientMenuService
    {
        public IEntityService<Guid, RestaurantMenuItemEntity, RestaurantMenuItemDto, IEntityRepository<Guid, RestaurantMenuItemEntity>, IEntityValidation<Guid, RestaurantMenuItemEntity>> RestaurantMenuItemService { get; set; }
        public IEntityService<Guid, RestaurantMenuEntity, RestaurantMenuDto, IEntityRepository<Guid, RestaurantMenuEntity>, IEntityValidation<Guid, RestaurantMenuEntity>> RestaurantMenuService { get; set; }
        public IEntityService<Guid, ClientTableEntity, ClientTableDto, IEntityRepository<Guid, ClientTableEntity>, IEntityValidation<Guid, ClientTableEntity>> ClientTableService { get; set; }

        public IEnumerable<RestaurantMenuItemDto> GetMenuList()
        {
            var restaurantMenu = this.GetRestaurantMenu();

            return restaurantMenu.MenuList;
        }

        public ClientMenuViewDto GetMenuView()
        {
            var restaurantMenu = this.GetRestaurantMenu();
            var clientTableSelectList = this.ClientTableService.GetSelectList();
            var result = new ClientMenuViewDto()
            {
                RestaurantMenu = restaurantMenu,
                ClientTableSelectList = clientTableSelectList
            };

            return result;
        }

        private RestaurantMenuDto GetRestaurantMenu()
        {
            var restaurantMenu = this.RestaurantMenuService.GetAll().FirstOrDefault();
            restaurantMenu.MenuList = this.RestaurantMenuItemService.GetByFilter(new
            {
                restaurant_menu_id = restaurantMenu.Id
            });

            return restaurantMenu;
        }
    }
}