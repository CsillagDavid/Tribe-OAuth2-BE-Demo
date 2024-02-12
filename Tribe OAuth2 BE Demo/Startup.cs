using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Tribe_OAuth2_BE_Demo.config.Database;
using Tribe_OAuth2_BE_Demo.DataAdapters;
using Tribe_OAuth2_BE_Demo.DtoMaps;
using Tribe_OAuth2_BE_Demo.Mappers;
using Tribe_OAuth2_BE_Demo.Models.Dtos;
using Tribe_OAuth2_BE_Demo.Repositories;
using Tribe_OAuth2_BE_Demo.Repositories.Interfaces;
using Tribe_OAuth2_BE_Demo.Services;

namespace Tribe_OAuth2_BE_Demo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddSingleton<IGoogleService, GoogleService>();
            services.AddSingleton<IAuthService, AuthService>();

            services.AddSingleton<IUserDataAdapter, UserDataAdapter>();
            services.AddSingleton<IUserDetailsDataAdapter, UserDetailsDataAdapter>();

            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IUserDetailsRepository, UserDetailsRepository>();

            services.AddSingleton<ISessionRepository, SessionRepository>();
            services.AddSingleton<ISessionDataAdapter, SessionDataAdapter>();

            services.AddSingleton<IUserService, UserService>();

            //services.AddSingleton<INHibernateRepository, NHibernateRepository>();

            var connectionString = _configuration.GetValue<string>("DatabaseSettings:ConnectionString");

            var config = MsSqlConfiguration
                .MsSql2012
                .ConnectionString(connectionString);

            var sessionFactory = Fluently.Configure()
            .Database(config)
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserDtoMap>())
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserDetailsDtoMap>())
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<SessionDtoMap>())
            //.Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
            .ExposeConfiguration(cfg => new SchemaExport(cfg)
                    .Create(false, false))
            .BuildSessionFactory();

            services.AddSingleton<ISession>(provider =>
            {
                return sessionFactory.OpenSession();
            });

            var mapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile(new DomainMappingProfile());
                }).CreateMapper();

            services.AddSingleton<IMapper>(mapper);

                    //services.AddAuthentication().AddFacebook(facebookOptions =>
                    //{
                    //    facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                    //    facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                    //    //facebookOptions.Fields = {"", ""};
                    //    //Fields = { "name", "email" },
                    //    //SaveTokens = true,
                    //});

                    EvolveConfigurer.Configure(_configuration);

            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = _configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = _configuration["Authentication:Facebook:AppSecret"];
                facebookOptions.AccessDeniedPath = "/AccessDeniedPathInfo";
                facebookOptions.SaveTokens = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // global cors policy
            app.UseCors(x => x
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
