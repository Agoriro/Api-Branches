namespace ApiBranch.Mappers
{
    public class BranchMapper
    {


        public int IdBranch { get; set; }

        public int BranchCode { get; set; }

        public string BranchDescription { get; set; } = null!;

        public string BranchAddress { get; set; } = null!;

        public string BranchId { get; set; } = null!;

        public string? BranchDateCreation { get; set; }

        public int? idCurrency { get; set; }
        public string? CurrencyName { get; set; }

    }
}
