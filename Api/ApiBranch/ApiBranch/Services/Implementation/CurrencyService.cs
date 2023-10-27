using Microsoft.EntityFrameworkCore;
using ApiBranch.Models;
using ApiBranch.Services.Contract;

namespace ApiBranch.Services.Implementation
{
    
    public class CurrencyService: ICurrencyService
    {
        private TestDbContext _dbContext;

        public CurrencyService(TestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CurrencyTest>> GetList()
        {
            try
            {
                List<CurrencyTest> list = new List<CurrencyTest>();
                list = await _dbContext.CurrencyTests.ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}
