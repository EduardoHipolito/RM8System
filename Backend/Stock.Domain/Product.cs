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
    public class Product : EntityBase
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(60)]
        [MinLength(4)]
        public string Name { get; set; }

        [MaxLength(30)]
        public string Color { get; set; }

        [MaxLength(30)]
        public string Brand { get; set; }

        [MaxLength(130)]
        public string Description { get; set; }

        [Required(ErrorMessage = "A categoria é obrigatoria")]
        [ForeignKey(nameof(FKCategory))]
        public int IdCategory { get; set; }
        public Category FKCategory { get; set; }

        public string InternalNumber { get; set; }

        [Required(ErrorMessage = "O codigo de barras é obrigatório")]
        public string BarCode { get; set; }

        [Required(ErrorMessage = "O tipo unitário é obrigatório")]
        public ProductUnityType UnityType { get; set; }

        [Required(ErrorMessage = "O tipo é obrigatório")]
        public ProductType ProductType { get; set; }

        [Required(ErrorMessage = "A embalagem é obrigatória")]
        public string Packing { get; set; }

        public decimal Weight { get; set; }

        public string MoreInformation { get; set; }

        public string Picture { get; set; }

        [Required(ErrorMessage = "O preço de custo é obrigatório")]
        public decimal CostPrice { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "O preço mínimo é obrigatório")]
        public decimal MinPrice { get; set; }
    }
}
