using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>()
            where T : class;

        void SaveChanges();

        IDbTransaction BeginTransaction();
    }

    public interface IDbTransaction : IDisposable
    {
        void Commit();

        void Rollback();
    }
}