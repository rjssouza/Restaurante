using Autofac;
using Autofac.Core;
using Autofac.Extras.Moq;
using Module.Dto.Configuration;
using Module.Dto.Enum;
using Module.IoC.Register;
using Module.IoC.Register.Interfaces.LifeCycle;
using System;
using System.Linq;

namespace Test.UnitTest.Base
{
    public abstract class BaseTest : ITestLifeCycle, IDisposable
    {
        public const string TEXT_LENGTH_100 = "Nam quis nulla. Integer malesuada. In in enim a arcu imperdiet malesuada. Sed vel lectus. Donec odio";
        public const string TEXT_LENGTH_255 = "Nam quis nulla. Integer malesuada. In in enim a arcu imperdiet malesuada. Sed vel lectus. Donec odio urna, tempus molestie, porttitor ut, iaculis quis, sem. Phasellus rhoncus. Aenean id metus id velit ullamcorper pulvinar. Vestibulum fermentum tortor id m";
        public const string TEXT_LENGTH_50 = "Nam quis nulla. Integer malesuada. In in enim a ar";
        public const string TEXT_LENGTH_500 = "Nam quis nulla. Integer malesuada. In in enim a arcu imperdiet malesuada. Sed vel lectus. Donec odio urna, tempus molestie, porttitor ut, iaculis quis, sem. Phasellus rhoncus. Aenean id metus id velit ullamcorper pulvinar. Vestibulum fermentum tortor id mi. Pellentesque ipsum. Nulla non arcu lacinia neque faucibus fringilla. Nulla non lectus sed nisl molestie malesuada. Proin in tellus sit amet nibh dignissim sagittis. Vivamus luctus egestas leo. Maecenas sollicitudin. Nullam rhoncus aliquam met";
        public const string TEXT_LENGTH_501 = "Nam quis nulla. Integer malesuada. In in enim a arcu imperdiet malesuada. Sed vel lectus. Donec odio urna, tempus molestie, porttitor ut, iaculis quis, sem. Phasellus rhoncus. Aenean id metus id velit ullamcorper pulvinar. Vestibulum fermentum tortor id mi. Pellentesque ipsum. Nulla non arcu lacinia neque faucibus fringilla. Nulla non lectus sed nisl molestie malesuada. Proin in tellus sit amet nibh dignissim sagittis. Vivamus luctus egestas leo. Maecenas sollicitudin. Nullam rhoncus aliquam mety";

        protected IContainer _container;
        private bool disposedValue;

        public BaseTest()
        {
            var builder = new ContainerBuilder();
            RegisterIoC.Register(builder, this);
        }

        public ConfigDto Config => new()
        {
            DbConnectionDto = new DbConnectionDto()
            {
                ConnectionString = "Server=localhost\\SQLEXPRESS;Database=orders;Trusted_Connection=True;",
                ConnectionType = TipoConexaoEnum.SqlServer
            },
            DbConnectionQueryDto = new DbConnectionDto()
            {
                ConnectionString = "Server=localhost\\SQLEXPRESS;Database=orders;Trusted_Connection=True;",
                ConnectionType = TipoConexaoEnum.SqlServer
            }
        };

        protected AutoMock Mock { get; set; }

        public void AbrirCicloVida(IContainer container)
        {
            container.BeginLifetimeScope();
        }

        public void Dispose()
        {
            Dispose(disposing: true);

            GC.SuppressFinalize(this);
        }

        public void OnActivatingInstance<TypeOf>(IActivatingEventArgs<TypeOf> e)
        {
            if (this.Mock == null)
                return;

            foreach (TypedService service in e.Component.Services)
            {
                var isRegistered = this.Mock.Container
                                            .ComponentRegistry
                                            .Registrations
                                            .Where(t => t.Services.Cast<TypedService>().Any(z => z.ServiceType == service.ServiceType))
                                            .Any();

                if (isRegistered)
                {
                    var newInstance = this.Mock.Container.Resolve(service.ServiceType);
                    e.ReplaceInstance(newInstance);
                }
            }
        }

        public void RegisterContainer(IContainer container)
        {
            this._container = container;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this._container.Dispose();
                    if (this.Mock != null)
                        this.Mock.Dispose();
                    
                    this._container = null;
                    this.Mock = null;
                }

                disposedValue = true;
            }
        }
    }
}