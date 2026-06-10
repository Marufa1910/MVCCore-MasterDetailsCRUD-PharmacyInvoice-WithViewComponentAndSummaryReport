using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace _1293481Evidence.Models.ViewModels
{
    public class InvoiceViewModel
    {
        public int InvoiceId { get; set; }

        [Required, Display(Name = "Invoice No")]
        public string InvoiceNo { get; set; } = null!;

        [Display(Name = "Transaction Date"), DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime TransactionDate { get; set; } = DateTime.Now;

        [Required, Display(Name = "Client Name")]
        public string ClientName { get; set; } = null!;

        [Display(Name = "Is New Client?")]
        public bool IsNewClient { get; set; }

        [Display(Name = "Referrer Name")]
        public string ReferrerName { get; set; } = null!;

        [Required, Display(Name = "Transaction Type")]
        public int TransactionTypeId { get; set; }

        [ValidateNever]
        public string? ImageUrl { get; set; }


        [ValidateNever]
        public virtual ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();


        [ValidateNever]
        public virtual TransactionType? TransactionType { get; set; }


        [ValidateNever]
        public List<TransactionType> TransactionTypes { get; set; } = new List<TransactionType>();


        [ValidateNever]
        [Display(Name = "Upload Image")]
        public IFormFile? ProfileFile { get; set; }
    }
}
