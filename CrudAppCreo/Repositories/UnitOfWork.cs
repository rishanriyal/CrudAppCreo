using CrudAppCreo.Models;

namespace CrudAppCreo.Repositories
{
    public interface IUnitOfWork
    {
        IEmployeeRepository EmployeeRepository { get; }
        ISalaryRepository SalaryRepository { get; }  
        void Save();
        Task<bool> SaveAsync();
        void Dispose();

    }
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        public readonly CrudAppCreoDbContext _dbContext;

        private IEmployeeRepository _EmployeeRepository;
        private ISalaryRepository _SalaryRepository;
        public UnitOfWork(CrudAppCreoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEmployeeRepository EmployeeRepository
        {
            get { return _EmployeeRepository = _EmployeeRepository ?? new EmployeeRepository(_dbContext); }
        }

        public ISalaryRepository SalaryRepository
        {
            get { return _SalaryRepository = _SalaryRepository ?? new SalaryRepository(_dbContext); }
        }

        private bool disposed = false;

        public void Save()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
