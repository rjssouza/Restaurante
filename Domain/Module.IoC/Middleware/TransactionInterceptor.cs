using System;
using Castle.DynamicProxy;
using Module.Factory.Interface.Connection;

namespace Module.IoC.Middleware
{
    public class TransactionInterceptor : IInterceptor
    {
        public IDbTransactionFactory TransactionFactory { get; set; }

        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (Exception)
            {
                this.TryRollback();

                throw;
            }
        }
        private void TryRollback()
        {
            try
            {
                this.TransactionFactory.Rollback();
            }
            finally { }
        }
    }
}