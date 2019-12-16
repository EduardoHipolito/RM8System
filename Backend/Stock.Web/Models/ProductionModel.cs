using Framework.Web.Models;
using System.Collections.Generic;

namespace Stock.Web.Models
{
    public class ProductionModel : BaseModel
    {
        public string Name { get; set; }
        
        public string Description { get; set; }

        public string MoreInformation { get; set; }

        public int IdFinalProduct { get; set; }
        public ProductModel FKFinalProduct { get; set; }

        public List<ProductionStageModel> ProductionStages { get; set; }
    }
}
