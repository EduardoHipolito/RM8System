using Framework.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stock.Domain
{
    public class ProductEntry : EntityBase
    {
        [Required(ErrorMessage = "O produto é obrigatório")]
        [ForeignKey(nameof(FKProduct))]
        public int IdProduct { get; set; }
        public Product FKProduct { get; set; }

        [ForeignKey(nameof(FKEntry))]
        public int IdEntry { get; set; }
        public Entry FKEntry { get; set; }

        [Required(ErrorMessage = "O preço unitário é obrigatório")]
        public decimal UnitPrice { get; set; }

        public decimal ICMS { get; set; }

        [Required(ErrorMessage = "O valor do IPI é obrigatório")]
        public decimal IPI { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatória")]
        public decimal Quantity { get; set; }
    }
}
