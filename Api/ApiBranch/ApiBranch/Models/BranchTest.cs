using System;
using System.Collections.Generic;

namespace ApiBranch.Models;

public partial class BranchTest
{
    public int IdBranch { get; set; }

    public int BranchCode { get; set; }

    public string BranchDescription { get; set; } = null!;

    public string BranchAddress { get; set; } = null!;

    public string BranchId { get; set; } = null!;

    public DateTime BranchDateCreation { get; set; }

    public int IdCurrency { get; set; }

    public virtual CurrencyTest IdCurrencyNavigation { get; set; } = null!;
}
