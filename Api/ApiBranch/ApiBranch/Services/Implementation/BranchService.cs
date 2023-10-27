using Microsoft.EntityFrameworkCore;
using ApiBranch.Models;
using ApiBranch.Services.Contract;

namespace ApiBranch.Services.Implementation
{
    public class BranchService : IBranchService
    {
        private TestDbContext _dbContext;

        public BranchService(TestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Devuelve Listado de Sucursales
        public async Task<List<BranchTest>> GetList()
        {
            try
            {
                List<BranchTest> list = new List<BranchTest>();
                list = await _dbContext.BranchTests.Include(cur => cur.IdCurrencyNavigation).ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        //Busca una sucursal por su Id
        public async Task<BranchTest> Get(int idBranch)
        {
            try
            {
                BranchTest? sucursal = new BranchTest();
                sucursal = await _dbContext.BranchTests.Include(ciu => ciu.IdCurrencyNavigation)
                    .Where(s => s.IdBranch == idBranch).FirstOrDefaultAsync();

                return sucursal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Agrega una sucursal
        public async Task<BranchTest> Add(BranchTest branch)
        {
            try
            {
                _dbContext.BranchTests.Add(branch);
                await _dbContext.SaveChangesAsync();
                return branch;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Actualiza una Sucursal
        public async Task<bool> Update(BranchTest branch)
        {
            try
            {
                _dbContext.BranchTests.Update(branch);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Elimina una sucursal
        public async Task<bool> Delete(BranchTest branch)
        {
            try
            {
                _dbContext.BranchTests.Remove(branch);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
