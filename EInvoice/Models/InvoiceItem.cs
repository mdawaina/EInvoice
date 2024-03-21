using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EInvoice.Models
{
    public class InvoiceItem
    {
       
        public int Id { get; set; }
        
        public int InvoiceId { get; set; }
       
        public Invoice Invoice { get; set; }
       
        public string ProductId { get; set; }
       
        public string Name { get; set; }
       
        public string NameEn { get; set; }
        
        public Product Product { get; set; }
        
        public double Quantity { get; set; }
        
        public decimal Price { get; set; }
       
        public decimal? CostPrice { get; set; }

    }
}
