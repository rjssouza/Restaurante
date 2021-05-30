using Module.Dto.Configuration;
using Module.Factory.Interface.Base;
using System;

namespace Module.Factory.Base
{
    public class BaseFactory : IBaseFactory
    {
        private bool disposedValue;

        public ConfigDto Config { get; set; }

        public BaseFactory()
        {

        }

        public void Dispose()
        {
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
    }
}