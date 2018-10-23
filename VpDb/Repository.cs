using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VpDb
{
    public class Repository : IRepository
    {
        private ISession _session;
        private ITransaction _trans;

        public Repository(DbFactory factory)
        {
            _session = factory.CreateSession();
            _trans = _session.BeginTransaction();
        }

        public void Dispose()
        {
            _trans.Commit();
            _session.Flush();
            _session.Close();
        }

        public IQueryable<T> Get<T>()
        {
            return _session.Query<T>();
        }

        public T Get<T>(int id)
        {
            return _session.Get<T>(id);
        }

        public void Save<T>(T entity)
        {
            _session.SaveOrUpdate(entity);
        }
    }
}
