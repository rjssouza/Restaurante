using Module.Dto.Configuration;
using Module.Factory.Interface.Connection;
using Module.Factory.Interface.Mapper;
using Module.Service.Interface.Base;
using System;

namespace Module.Service.Base
{
    public abstract class BaseService : IBaseService
    {
        private bool disposedValue;

        public ConfigDto Config { get; set; }
        public IDbTransactionFactory DbTransactionFactory { get; set; }
        public IObjectConverter ObjectConverter { get; set; }

        public void Dispose()
        {
            // Não altere este código. Coloque o código de limpeza no método 'Dispose(bool disposing)'
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected void BeginTransaction()
        {
            this.DbTransactionFactory.BeginTransaction();
        }

        protected void Commit()
        {
            this.DbTransactionFactory.Commit();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                disposedValue = true;
            }
        }

        protected void Rollback()
        {
            this.DbTransactionFactory.Rollback();
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~BaseService()
        // {
        //     // Não altere este código. Coloque o código de limpeza no método 'Dispose(bool disposing)'
        //     Dispose(disposing: false);
        // }
    }
}