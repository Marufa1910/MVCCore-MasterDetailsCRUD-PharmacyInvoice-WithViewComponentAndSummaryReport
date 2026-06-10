using System;
using System.Collections.Generic;

namespace _1293481Evidence.Models;

public partial class SaleItem
{
    public int SaleItemId { get; set; }

    public string MedicineName { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public int InvoiceId { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;
}
