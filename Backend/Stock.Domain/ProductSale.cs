using Core.Domain.Enum;
using Framework.Domain;
using Stock.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stock.Domain
{
    public class ProductSale : EntityBase
    {
        [Required(ErrorMessage = "O produto é obrigatorio")]
        [ForeignKey(nameof(FKProduct))]
        public int IdProduct { get; set; }
        public Product FKProduct { get; set; }

        [Required(ErrorMessage = "O produto é obrigatorio")]
        [ForeignKey(nameof(FKSale))]
        public int SaleId { get; set; }
        public Sale FKSale { get; set; }

        public int Quantity { get; set; }
    }
}
