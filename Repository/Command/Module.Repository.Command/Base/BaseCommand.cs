using System;
using Autofac.Features.AttributeFilters;
using Module.Factory.Interface.Connection;
using Module.Factory.Interface.Mapper;
using Module.Repository.Command.Interface.Base;
using Module.Repository.Entity.Base;

namespace Module.Repository.Command.Base
{
    public abstract class BaseCommand<TReturn, TKey, TEntity> : IBaseCommand
        where TEntity : BaseEntity<TKey>
    {
        protected IDbConnectionFactory _connectionFactory;
       
        public IObjectConverter ObjectConverter { get; set; }
       
        private bool disposedValue;

        public BaseCommand([KeyFilter("Command")]IDbConnectionFactory connectionFactory)
        {
            this._connectionFactory = connectionFactory;
        }

        public abstract TReturn Execute(TEntity entity);

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~BaseCommand()
        // {
        //     // Não altere este código. Coloque o código de limpeza no método 'Dispose(bool disposing)'
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Não altere este código. Coloque o código de limpeza no método 'Dispose(bool disposing)'
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}