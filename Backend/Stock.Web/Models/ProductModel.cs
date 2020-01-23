using Core.Domain.Enum;
using Framework.Domain;
using Framework.Web.Models;
using Stock.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stock.Web.Models
{
    public class ProductModel : BaseModel
    {
        public string Name { get; set; }

        public string Color { get; set; }

        public string brand { get; set; }

        public string Description { get; set; }

        public int IdCategory { get; set; }
        public CategoryModel FKCategory { get; set; }

        public string InternalNumber { get; set; }

        public string BarCode { get; set; }

        public ProductUnityType UnityType { get; set; }

        public ProductType ProductType { get; set; }

        public string Packing { get; set; }

        public decimal? Weight { get; set; }

        public string MoreInformation { get; set; }

        public string Picture { get; set; }

        public decimal CostPrice { get; set; }

        public decimal Price { get; set; }

        public decimal MinPrice { get; set; }
    }
}
