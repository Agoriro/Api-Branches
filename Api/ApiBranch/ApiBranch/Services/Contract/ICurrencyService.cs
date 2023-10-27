using ApiBranch.Models;

namespace ApiBranch.Services.Contract
{
    public interface ICurrencyService
    {
        Task<List<CurrencyTest>> GetList();
    }
}
