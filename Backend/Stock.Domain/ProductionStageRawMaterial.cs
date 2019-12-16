using Framework.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.Domain
{
    public class ProductionStageRawMaterial : EntityBase
    {
        [ForeignKey(nameof(FKProductionStage))]
        public int IdProductionStage { get; set; }
        public ProductionStage FKProductionStage { get; set; }

        [Required(ErrorMessage = "A matéria prima é obrigatoria")]
        [ForeignKey(nameof(FKProduct))]
        public int IdProduct { get; set; }
        public Product FKProduct { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatoria")]
        public int Quantity { get; set; }

    }
}
