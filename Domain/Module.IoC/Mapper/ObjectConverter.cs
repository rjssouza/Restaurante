using AutoMapper;
using Module.Dto;
using Module.Dto.Base;
using Module.Factory.Interface.Mapper;
using Module.IoC.Register.Interfaces;
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
            mapperConfigExpression.CreateMap<BaseDto<Guid>, GenericSelectDto<Guid>>()
                .ForMember(src => src.Text, dest => dest.MapFrom(t => t.ToString()));
        }
    }
}