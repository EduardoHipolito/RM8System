using Framework.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stock.Web.Models
{
    public class ProductionStageModel : BaseModel
    {
        public int IdProduction { get; set; }
        public ProductionModel FKProduction { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? IdSupplier { get; set; }
        public SupplierModel FKSupplier { get; set; }

        public decimal? SupplierCost { get; set; }

        public int HoursLong { get; set; }

        public List<ProductionStageRawMaterialModel> ProductionStageRawMaterials { get; set; }
    }
}
