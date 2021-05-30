using System;
using Autofac.Features.AttributeFilters;
using Module.Factory.Interface.Connection;
using Module.Factory.Interface.Mapper;
using Module.Repository.Query.Interface.Base;

namespace Module.Repository.Query.Base
{
    public abstract class BaseQuery : IBaseQuery
    {
        protected IDbConnectionFactory _connectionFactory;

        public IObjectConverter ObjectConverter { get; set; }
        
        public BaseQuery([KeyFilter("Query")] IDbConnectionFactory connectionFactory)
        {
            this._connectionFactory = connectionFactory;
        }

        private bool disposedValue;

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
        // ~BaseQuery()
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