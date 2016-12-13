using System.Data.Entity;
using System.Transactions;
using TrustSystems3.Repository.Infrastructure.Contract;

namespace TrustSystems3.Repository.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private TransactionScope _transaction;
        private readonly TSModelContainer _db;

        public UnitOfWork()
        {
            _db = new TSModelContainer();
        }

        public void Dispose()
        {
           
        }

        public void StartTransaction()
        {
            _transaction = new TransactionScope();
        }

        public void Commit()
        {
            _db.SaveChanges();
            _transaction.Complete();
        }

        public DbContext Db
        {
            get { return _db; }
        }
    }
}