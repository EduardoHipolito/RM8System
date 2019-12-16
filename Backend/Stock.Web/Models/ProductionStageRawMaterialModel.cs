using Framework.Web.Models;

namespace Stock.Web.Models
{
    public class ProductionStageRawMaterialModel : BaseModel
    {
        public int IdProductionStage { get; set; }
        public ProductionStageModel FKProductionStage { get; set; }

        public int IdProduct { get; set; }
        public ProductModel FKProduct { get; set; }

        public int Quantity { get; set; }
    }
}
