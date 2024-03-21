using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace EInvoice.Models
{
    public class Invoice
    {
       
        public int Id { get; set; }
       
        public int? InvoiceNumber { get; set; }
        
        public string Name { get; set; }
        
        public int? OrderId { get; set; }
                  
        public int CompanyId { get; set; }
       
        public int? CompanyBranchId { get; set; }
        public int? DiscountType { get; set; } = 1;
                
        public double? DiscountPercentage { get; set; }
        
        public decimal? Discount { get; set; }
       
        public decimal VATAmount { get; set; }
        
        public decimal? SubtotalBeforeVat { get; set; }
        
        public decimal Subtotal { get; set; }

        public decimal Total { get; set; }
        public decimal? TotalCostPrice { get; set; }

        public string InvoiceNotes { get; set; }
        
        public string SellerId { get; set; }
        
        public string BuyerId { get; set; }
        public bool IsDeleted { get; set; }
        
        public DateTime CreatedOn { get; set; }
       
        public string CreatedBy { get; set; }
        
        public DateTime UpdatedOn { get; set; }
       
        public string UpdatedBy { get; set; }
       
        public string TLVBase64 { get; set; }

        public ICollection<InvoiceItem> InvoiceItems { get; set; }

    }
}
