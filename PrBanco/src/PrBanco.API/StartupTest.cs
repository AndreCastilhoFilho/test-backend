using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PrBanco.API.AutoMapper;
using PrBanco.API.Services;
using PrBanco.Data.MongoDataAccess;
using PrBanco.Data.MongoDataAccess.Context;
using PrBanco.Data.MongoDataAccess.Repositories;
using PrBanco.Domain.Repositories;
using System.Reflection;

namespace PrBanco.API
{
    public class StartupTest
    {

        public StartupTest(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.Development.json", optional: true)
            .AddEnvironmentVariables();

            Configuration = builder.Build();
            Environment = env;
        }

        public IConfigurationRoot Configuration { get; }
        public IHostingEnvironment Environment { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                 .AddJsonOptions(options =>
                 {
                     options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                     options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                 })
            ;

            //services.AddAutoMapperSetup();
            var assembly = typeof(Program).GetTypeInfo().Assembly;
            services.AddAutoMapper(assembly);
            Mapper.Reset();
            AutoMapperConfig.RegisterMappings();
            services.AddOptions();

            RegisterServices(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseDeveloperExceptionPage();

            app.UseCors(c =>
          {
              c.AllowAnyHeader();
              c.AllowAnyMethod();
              c.AllowAnyOrigin();
          });

            app.UseStaticFiles();

            app.UseMvc();
        }


        private void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //Settings
            services.Configure<DbSettings>(options =>
            {
                options.ConnectionString = Configuration.GetSection("MongoConnection:ConnectionString").Value;
                options.Database = Configuration.GetSection("MongoConnection:Database").Value;
            });

            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPersonRepository, PersonRepository>();

            //Seed Database
            services.AddSingleton<PrBancoDbContext>();
            var provider = services.BuildServiceProvider();
            var dbContext = provider.GetService<PrBancoDbContext>();
            dbContext.Seed();

        }
    }
}
