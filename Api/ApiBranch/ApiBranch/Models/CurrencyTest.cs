using System;
using System.Collections.Generic;

namespace ApiBranch.Models;

public partial class CurrencyTest
{
    public int IdCurrency { get; set; }

    public string CurrencyName { get; set; } = null!;

    public string CurrencySymbol { get; set; } = null!;

    public virtual ICollection<BranchTest> BranchTests { get; set; } = new List<BranchTest>();
}
