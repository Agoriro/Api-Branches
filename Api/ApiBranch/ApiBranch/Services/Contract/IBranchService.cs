using ApiBranch.Models;

namespace ApiBranch.Services.Contract
{
    public interface IBranchService
    {
        Task<List<BranchTest>> GetList();
        Task<BranchTest> Get(int idBranch);

        Task<BranchTest> Add(BranchTest branch);

        Task<bool> Update(BranchTest branch);

        Task<bool> Delete(BranchTest branch);

    }
}
