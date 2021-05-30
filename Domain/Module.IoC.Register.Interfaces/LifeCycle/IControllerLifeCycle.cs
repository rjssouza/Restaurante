using System;
using Autofac;

namespace Module.IoC.Register.Interfaces.LifeCycle
{
    public interface IControllerLifeCycle : IContainerLifeCycle
    {
        void RegisterControllers(ContainerBuilder builder);
    }
}