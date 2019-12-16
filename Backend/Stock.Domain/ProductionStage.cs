using Framework.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.Domain
{
    public class ProductionStage : EntityBase
    {
        [ForeignKey(nameof(FKProduction))]
        public int IdProduction { get; set; }
        public Production FKProduction { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(60)]
        [MinLength(4)]
        public string Name { get; set; }

        [MaxLength(130)]
        public string Description { get; set; }

        [ForeignKey(nameof(FKSupplier))]
        public int? IdSupplier { get; set; }
        public Supplier FKSupplier { get; set; }

        public decimal? SupplierCost { get; set; }

        public int HoursLong { get; set; }

        public ICollection<ProductionStageRawMaterial> ProductionStageRawMaterials { get; set; }
    }
}
