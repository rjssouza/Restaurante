using System;
using Module.Dto.CustomException.Validation;
using Module.Service.Validation.Interface.Base;

namespace Module.Service.Validation.Base
{
    public abstract class BaseValidation : IBaseValidation
    {
        private bool disposedValue;
        private readonly Summary _summary = new();

        protected void AddError(string key, string message)
        {
            this._summary.AddError(key, message);
        }

        protected void AddError(string message)
        {
            this._summary.AddError(message);
        }

        protected void Validate()
        {
            if (this._summary.ContainsErrors)
                throw new ValidationException(this._summary);
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

        public void Dispose()
        {
            // Não altere este código. Coloque o código de limpeza no método 'Dispose(bool disposing)'
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}