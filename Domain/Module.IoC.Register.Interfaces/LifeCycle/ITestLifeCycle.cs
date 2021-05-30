using System;
using Autofac;
using Autofac.Core;

namespace Module.IoC.Register.Interfaces.LifeCycle
{
    public interface ITestLifeCycle : IContainerLifeCycle
    {
        void OnActivatingInstance<TypeOf>(IActivatingEventArgs<TypeOf> e);        
        void RegisterContainer(IContainer container);
        void AbrirCicloVida(IContainer container);
    }
}