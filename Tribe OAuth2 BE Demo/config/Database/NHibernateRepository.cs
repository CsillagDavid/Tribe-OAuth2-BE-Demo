using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Engine;
using NHibernate.Metadata;
using NHibernate.Stat;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Tribe_OAuth2_BE_Demo.DtoMaps;
using Tribe_OAuth2_BE_Demo.Models.Dtos;

namespace Tribe_OAuth2_BE_Demo.config.Database
{
    public class NHibernateRepository : INHibernateRepository
    {
        public ISession _session { get; }
        public NHibernateRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("DatabaseSettings:ConnectionString");

            var config = MsSqlConfiguration
                .MsSql2012
                .ConnectionString(connectionString);

            var _sessionFactory = Fluently.Configure()
            .Database(config)
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserDtoMap>())
            //.Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
            .ExposeConfiguration(cfg => new SchemaExport(cfg)
                    .Create(false, false))
            .BuildSessionFactory();

            _session = _sessionFactory.OpenSession();

        }

        public T Save<T>(T dto)
        {
            T savedDto;
            using (var tx = _session.BeginTransaction())
            {
                try
                {
                    savedDto = (T)_session.Save(dto);
                    tx.Commit();
                }
                catch (Exception)
                {
                    tx.Rollback();
                    throw;
                }
            }

            return savedDto;
        }
        
    }
}
