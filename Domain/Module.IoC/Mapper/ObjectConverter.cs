using AutoMapper;
using Module.Dto;
using Module.Factory.Interface.Mapper;
using Module.IoC.Register.Interfaces;
using Module.Repository.Entity;
using Module.Repository.Entity.Base;
using System;

namespace Module.IoC.Mapper
{
    public class ObjectConverter : IObjectConverter
    {
        private readonly IMapper _mapper;
        private bool disposedValue;

        public ObjectConverter()
        {
            var mapperConfiguration = new MapperConfiguration(ConfigureMapper);

            this._mapper = mapperConfiguration.CreateMapper();
        }

        public IObjectMoq ObjectMoq { get; set; }

        public T ConvertTo<T>(object source)
            where T : class
        {
            var result = this._mapper.Map<T>(source);
            var mockedResult = this.ObjectMoq.GetMoqObject<T>(result);

            return mockedResult ?? result;
        }

        public void Dispose()
        {
            // Não altere este código. Coloque o código de limpeza no método 'Dispose(bool disposing)'
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                disposedValue = true;
            }
        }

        private void ConfigureMapper(IMapperConfigurationExpression mapperConfigExpression)
        {
            ///Conversor global para seleção generica usando construtor generico
            mapperConfigExpression.CreateMap<BaseEntity<Guid>, GenericSelectDto<Guid>>()
                .ForMember(src => src.Text, dest => dest.MapFrom(t => t.ToString()));

            mapperConfigExpression.CreateMap<ClientTableEntity, ClientTableDto>();
            mapperConfigExpression.CreateMap<ClientTableDto, ClientTableEntity>();

            mapperConfigExpression.CreateMap<OrderCommandEntity, OrderCommandDto>();
            mapperConfigExpression.CreateMap<OrderCommandDto, OrderCommandEntity>();

            mapperConfigExpression.CreateMap<OrderCommandItemEntity, OrderCommandItemDto>();
            mapperConfigExpression.CreateMap<OrderCommandItemDto, OrderCommandItemEntity>();

            mapperConfigExpression.CreateMap<OrderCommandPaymentEntity, OrderCommandPaymentDto>();
            mapperConfigExpression.CreateMap<OrderCommandPaymentDto, OrderCommandPaymentEntity>();
 
            mapperConfigExpression.CreateMap<RestaurantMenuEntity, RestaurantMenuDto>();
            mapperConfigExpression.CreateMap<RestaurantMenuDto, RestaurantMenuEntity>();

            mapperConfigExpression.CreateMap<RestaurantMenuItemEntity, RestaurantMenuItemDto>();
            mapperConfigExpression.CreateMap<RestaurantMenuItemDto, RestaurantMenuItemEntity>();
        }
    }
}