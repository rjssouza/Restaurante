using System;
using System.Linq;
using Module.Dto;
using Module.Repository.Entity;
using Module.Repository.Interface.Base;
using Module.Service.Base;
using Module.Service.Interface;
using Module.Service.Interface.Base;
using Module.Service.Internal;
using Module.Service.Validation.Interface.Base;

namespace Module.Service
{
    public class MenuService : BaseService, IMenuService
    {
        public IEntityService<Guid, RestaurantMenuEntity, RestaurantMenuDto, IEntityRepository<Guid, RestaurantMenuEntity>, IEntityValidation<Guid, RestaurantMenuEntity>> RestaurantMenuService { get; set; }
        public IEntityService<Guid, RestaurantMenuItemEntity, RestaurantMenuItemDto, IEntityRepository<Guid, RestaurantMenuItemEntity>, IEntityValidation<Guid, RestaurantMenuItemEntity>> RestaurantMenuItemService { get; set; }
        
        public ClientMenuViewDto GetMenuView()
        {
            var restaurantMenu = this.RestaurantMenuService.GetAll().FirstOrDefault();
            var restaurantMenuItemList = this.RestaurantMenuItemService.GetByFilter(new 
            {
                restaurant_menu_id = restaurantMenu.Id
            });
            
            var result = new ClientMenuViewDto()
            {
                RestaurantMenu = restaurantMenu,
                MenuList = restaurantMenuItemList
            };

            return result;
        }
    }
}