using Autofac;
using Autofac.Core;
using Autofac.Extras.DynamicProxy;
using Autofac.Features.AttributeFilters;
using Connection;
using Module.Dto.Configuration;
using Module.Factory.Base;
using Module.Factory.Interface.Connection;
using Module.Factory.Interface.Mapper;
using Module.Integration.Base;
using Module.IoC.Mapper;
using Module.IoC.Middleware;
using Module.IoC.Register.Interfaces;
using Module.IoC.Register.Interfaces.LifeCycle;
using Module.Repository.Base;
using Module.Repository.Command.Base;
using Module.Repository.Command.Interface.Base;
using Module.Repository.Interface.Base;
using Module.Repository.Query.Base;
using Module.Repository.Query.Interface.Base;
using Module.Service.Base;
using Module.Service.Interface.Base;
using Module.Service.Internal;
using Module.Service.Validation.Base;
using Module.Service.Validation.Interface.Base;
using System.Reflection;

namespace Module.IoC
{
    public class AutofacRegister
    {
        private IContainer _container;
        private ContainerBuilder _containerBuilder;
        private IContainerLifeCycle _containerLifeCycle;

        public void Register(ContainerBuilder builder, IContainerLifeCycle containerLifeCycle)
        {
            this._containerBuilder = builder;
            this._containerLifeCycle = containerLifeCycle;
            this.RegisterModules();

            if (containerLifeCycle is IControllerLifeCycle controllerLifeCycle)
                this.RegisterControllerLifeCycle(controllerLifeCycle);

            if (containerLifeCycle is ITestLifeCycle testLifeCycle)
                this.RegisterTestLifeCycle(testLifeCycle);
        }

        private void OnActivatingInstanceForTesting<TypeOf>(IActivatingEventArgs<TypeOf> e)
        {
            if (this._containerLifeCycle is ITestLifeCycle testLifeCycle)
                testLifeCycle.OnActivatingInstance<TypeOf>(e);
        }

        private void RegisterControllerLifeCycle(IControllerLifeCycle controllerLifeCycle)
        {
            controllerLifeCycle.RegisterControllers(this._containerBuilder);
        }

        private void RegisterModules()
        {
            this._containerBuilder.Register<ConfigDto>(c => this._containerLifeCycle.Config)
                .AsSelf()
                .SingleInstance();

            this._containerBuilder.RegisterType<ObjectMoq>()
                .As<IObjectMoq>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .SingleInstance();

            this._containerBuilder.RegisterType<ObjectConverter>()
                .As<IObjectConverter>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .SingleInstance();

            this._containerBuilder.RegisterType<TransactionInterceptor>()
                .AsSelf()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            this._containerBuilder.RegisterAssemblyTypes(Assembly.Load(typeof(BaseFactory).Assembly.GetName()))
                .Where(t => t.Name.EndsWith("Factory"))
                .OnActivating(OnActivatingInstanceForTesting)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            this._containerBuilder.RegisterType<DbConnectionFactory>()
                .As<IDbConnectionFactory>()
                .As<IDbTransactionFactory>()
                .UsingConstructor(typeof(DbConnectionDto))
                .Keyed<IDbConnectionFactory>("Command")
                .Keyed<IDbTransactionFactory>("Command")
                .InstancePerLifetimeScope()
                .WithParameters(new[] {
                    new NamedParameter("dbConnectionDto", this._containerLifeCycle.Config.DbConnectionDto)
                });

            this._containerBuilder.RegisterType<DbConnectionFactory>()
                .As<IDbConnectionFactory>()
                .UsingConstructor(typeof(DbConnectionDto))
                .Keyed<IDbConnectionFactory>("Query")
                .InstancePerLifetimeScope()
                .WithParameters(new[] {
                    new NamedParameter("dbConnectionDto", this._containerLifeCycle.Config.DbConnectionQueryDto)
                });

            this._containerBuilder.RegisterAssemblyTypes(Assembly.Load(typeof(BaseCommand<,,>).Assembly.GetName()))
                .Where(t => t.Name.EndsWith("Command"))
                .OnActivating(OnActivatingInstanceForTesting)
                .AsImplementedInterfaces()
                .WithAttributeFiltering()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            this._containerBuilder.RegisterAssemblyTypes(Assembly.Load(typeof(BaseQuery).Assembly.GetName()))
                .Where(t => t.Name.EndsWith("Query"))
                .OnActivating(OnActivatingInstanceForTesting)
                .AsImplementedInterfaces()
                .WithAttributeFiltering()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            this._containerBuilder.RegisterGeneric(typeof(DeleteCommand<,>))
                .As(typeof(IDeleteCommand<,>))
                .WithAttributeFiltering()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            this._containerBuilder.RegisterGeneric(typeof(InsertCommand<,>))
                .As(typeof(IInsertCommand<,>))
                .WithAttributeFiltering()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            this._containerBuilder.RegisterGeneric(typeof(UpdateCommand<,>))
                .As(typeof(IUpdateCommand<,>))
                .WithAttributeFiltering()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            this._containerBuilder.RegisterGeneric(typeof(GetAllQuery<,>))
                .As(typeof(IGetAllQuery<,>))
                .WithAttributeFiltering()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            this._containerBuilder.RegisterGeneric(typeof(GetAllByFilterQuery<,>))
                .As(typeof(IGetAllByFilterQuery<,>))
                .WithAttributeFiltering()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            this._containerBuilder.RegisterGeneric(typeof(GetByIdQuery<,>))
                .As(typeof(IGetByIdQuery<,>))
                .WithAttributeFiltering()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            this._containerBuilder.RegisterGeneric(typeof(GetFirstByFilterQuery<,>))
                .As(typeof(IGetFirstByFilterQuery<,>))
                .WithAttributeFiltering()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            this._containerBuilder.RegisterGeneric(typeof(GetSelectListQuery<,>))
                .As(typeof(IGetSelectListQuery<,>))
                .WithAttributeFiltering()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            this._containerBuilder.RegisterAssemblyTypes(Assembly.Load(typeof(BaseRepository).Assembly.GetName()))
                .Where(t => t.Name.EndsWith("Repository"))
                .OnActivating(OnActivatingInstanceForTesting)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            this._containerBuilder.RegisterAssemblyTypes(Assembly.Load(typeof(BaseValidation).Assembly.GetName()))
                .Where(t => t.Name.EndsWith("Validation"))
                .OnActivating(OnActivatingInstanceForTesting)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            this._containerBuilder.RegisterAssemblyTypes(Assembly.Load(typeof(BaseIntegration).Assembly.GetName()))
                .Where(t => t.Name.EndsWith("Integration"))
                .OnActivating(OnActivatingInstanceForTesting)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            this._containerBuilder.RegisterAssemblyTypes(Assembly.Load(typeof(BaseService).Assembly.GetName()))
                .Where(t => t.Name.EndsWith("Service"))
                .OnActivating(OnActivatingInstanceForTesting)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(TransactionInterceptor))
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            this._containerBuilder.RegisterGeneric(typeof(EntityRepository<,>))
                .As(typeof(IEntityRepository<,>))
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            this._containerBuilder.RegisterGeneric(typeof(EntityValidation<,>))
                .As(typeof(IEntityValidation<,>))
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            this._containerBuilder.RegisterGeneric(typeof(EntityValidationService<,,,,>))
                .As(typeof(IEntityService<,,,,>))
                .InstancePerLifetimeScope()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(TransactionInterceptor))
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
        }

        private void RegisterTestLifeCycle(ITestLifeCycle testLifeCycle)
        {
            this._container = this._containerBuilder.Build();
            testLifeCycle.AbrirCicloVida(this._container);

            testLifeCycle.RegisterContainer(this._container);
        }
    }
}