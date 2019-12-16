using Framework.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stock.Domain
{
    public class Sale : EntityBase
    {
        [ForeignKey(nameof(FKCustomer))]
        public int? IdCustomer { get; set; }
        public Customer FKCustomer { get; set; }

        public decimal Discount { get; set; } = 0;

        [Required(ErrorMessage = "O preço do frete é obrigatório")]
        public decimal Shipping { get; set; }

        public ICollection<ProductSale> Products { get; set; }
        public ICollection<Payment> Payments { get; set; }

    }
}
