using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VpDb
{
    public interface IRepository : IDisposable
    {
        IQueryable<T> Get<T>();
        T Get<T>(int id);
        void Save<T>(T entity);
    }
}
