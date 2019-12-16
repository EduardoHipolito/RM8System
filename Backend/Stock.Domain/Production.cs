using Framework.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.Domain
{
    public class Production : EntityBase
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(60)]
        [MinLength(4)]
        public string Name { get; set; }

        [MaxLength(130)]
        public string Description { get; set; }

        public string MoreInformation { get; set; }

        [Required(ErrorMessage = "O produto final é obrigatoria")]
        [ForeignKey(nameof(FKFinalProduct))]
        public int IdFinalProduct { get; set; }
        public Product FKFinalProduct { get; set; }

        public ICollection<ProductionStage> ProductionStages { get; set; }
    }
}
