using System;
using System.Collections.Generic;

namespace _1293481Evidence.Models;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public string InvoiceNo { get; set; } = null!;

    public DateTime TransactionDate { get; set; }

    public string ClientName { get; set; } = null!;

    public bool IsNewClient { get; set; }

    public string ReferrerName { get; set; } = null!;

    public int TransactionTypeId { get; set; }

    public string? ImageUrl { get; set; }

    public virtual ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();

    public virtual TransactionType TransactionType { get; set; } = null!;
}
