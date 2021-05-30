using Autofac;
using Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Module.Dto.Configuration;
using Module.Dto.Enum;
using Module.IoC.Register;
using Module.IoC.Register.Interfaces.LifeCycle;
using Module.Utils.Json;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using WebApi.Attribute;
using WebApi.Filter;

namespace WebApi
{
    public class Startup : IControllerLifeCycle
    {
        /// <summary>
        /// Construtor classe startup
        /// </summary>
        /// <param name="env">Configurações de ambiente</param>
        public Startup(IWebHostEnvironment env)
        {
#if DEBUG
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
#elif PRODUCTION
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.Production.json", optional: true)
                .AddEnvironmentVariables();
#elif RELEASE
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.Release.json", optional: true)
                .AddEnvironmentVariables();
#endif

            this.Configuration = builder.Build();
        }

        /// <summary>
        /// Configurações da aplicação
        /// </summary>
        private ConfigDto _config;

        /// <summary>
        /// Configuration root
        /// </summary>
        public IConfigurationRoot Configuration { get; private set; }

        /// <summary>
        /// Configurações da aplicação
        /// </summary>
        public ConfigDto Config => this._config;

        /// <summary>
        /// Método para registrar serviços .net core
        /// </summary>
        /// <param name="services">Registrador de servi�os</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddControllers();

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ExceptionFilter));
            })
            .AddControllersAsServices()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.Converters.Add(new JsonConverterByteArrayGlobal());
            });

            services.AddHttpContextAccessor();
            services.AddHttpClient();
            services.AddLogging();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Locação de Imóveis Api",
                    Description = "Serviço para cadastro e locação ou compra de imóveis utilizando ASP.NET Core Web API",
                    Contact = new OpenApiContact
                    {
                        Name = "Robson Souza",
                        Email = "robsonjesus908@gmail.com",
                        Url = new Uri("https://github.com/rjssouza/HouseRent"),
                    }
                });

                c.EnableAnnotations();
                c.OperationFilter<CustomHeaderSwaggerAttribute>();

                var security = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        Array.Empty<string>()
                    }
                };

                c.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = "Copie 'Bearer ' + token'",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });

                c.AddSecurityRequirement(security);

                foreach (var name in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.SwaggerDoc.XML", SearchOption.AllDirectories))
                {
                    c.IncludeXmlComments(name);
                }
            });

            this.RegisterConfig();

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        /// <summary>
        /// Método para configurar aplicação
        /// </summary>
        /// <param name="app">Serviço de construtor da aplicação</param>
        /// <param name="env">Serviço de ambiente da aplicação</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
               .AllowAnyMethod()
               .AllowAnyHeader()
               .SetIsOriginAllowed(origin => true) // allow any origin
               .AllowCredentials()); // allow credentials

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Locação Imóveis API V1");
                c.RoutePrefix = string.Empty;
            });
        }

        /// <summary>
        /// Chamada da implementação para configurar container (chamada efetuada pelo m�dulo Module.Ioc)
        /// </summary>
        /// <param name="builder">Container builder autofac</param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            RegisterIoC.Register(builder, this);
        }

        /// <summary>
        /// Método para registrar controllers autofac
        /// </summary>
        /// <param name="builder">Container builder</param>
        public void RegisterControllers(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load(typeof(ServiceController).Assembly.GetName()))
                   .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
        }

        /// <summary>
        /// Mètodo para registrar config
        /// </summary>
        private void RegisterConfig()
        {
            var dbConnectionDto = this.Configuration.GetSection("DbConnectionDto");
            var dbConnectionQueryDto = this.Configuration.GetSection("DbConnectionQueryDto");

            this._config = new ConfigDto()
            {
                DbConnectionDto = new DbConnectionDto()
                {
                    ConnectionString = dbConnectionDto.GetValue<string>("ConnectionString"),
                    ConnectionType = dbConnectionDto.GetValue<TipoConexaoEnum>("ConnectionType")
                },
                DbConnectionQueryDto = new DbConnectionDto()
                {
                    ConnectionString = dbConnectionQueryDto.GetValue<string>("ConnectionString"),
                    ConnectionType = dbConnectionQueryDto.GetValue<TipoConexaoEnum>("ConnectionType")
                }
            };
        }
    }
}