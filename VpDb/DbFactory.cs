using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VpDb.Entities;

namespace VpDb
{
    public class DbFactory
    {
        private ISessionFactory _factory;

        public DbFactory(string connectionString)
        {
            _factory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                .ConnectionString(connectionString))
                .Mappings(
                    m =>
                    m.FluentMappings.AddFromAssemblyOf<Game>())
                .BuildConfiguration()
                .BuildSessionFactory();
        }

        public ISession CreateSession()
        {
            return _factory.OpenSession();
        }
    }
}
