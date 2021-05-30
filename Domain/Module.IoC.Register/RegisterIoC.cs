using System;
using Autofac;
using Module.IoC.Register.Interfaces.LifeCycle;

namespace Module.IoC.Register
{
    public static class RegisterIoC
    {
        public static void Register(ContainerBuilder builder, IContainerLifeCycle containerLifeCycle)
        {
            var autofacRegister = new AutofacRegister(); 
             
            autofacRegister.Register(builder, containerLifeCycle);     
        }
    }
}