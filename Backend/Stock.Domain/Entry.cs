using Framework.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stock.Domain
{
    public class Entry : EntityBase
    {
        [ForeignKey(nameof(FKSupplier))]
        public int? IdSupplier { get; set; }
        public Supplier FKSupplier { get; set; }

        public decimal Discount { get; set; } = 0;

        [Required(ErrorMessage = "O preço do frete é obrigatório")]
        public decimal Shipping { get; set; } = 0;

        public ICollection<ProductEntry> ProductEntries { get; set; }
        public bool IsCanceled { get; set; }

        [NotMapped]
        public decimal TotalPrice
        {
            get
            {
                decimal total = 0;
                foreach (var f in ProductEntries)
                {
                    total += (f.ICMS + f.IPI + (f.UnitPrice * f.Quantity));

                }
                total +=  Shipping;
                total -=  Discount;

                return total;
            }
        }
    }
}
