using CrudAppCreo.Models;

namespace CrudAppCreo.Repositories
{
    public interface ISalaryRepository : IGenericRepository<Salary>
    {

    }
    public class SalaryRepository : GenericRepository<Salary>, ISalaryRepository
    {
        CrudAppCreoDbContext _dbContext;
        public SalaryRepository(CrudAppCreoDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
