using Framework.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stock.Web.Models
{
    public class FormOfPaymentModel : BaseModel
    {
        public string Name { get; set; }

        public int? MinNumberOfInstallments { get; set; }

        public int? MaxNumberOfInstallments { get; set; }

        public decimal? MinimumValue { get; set; }

        public string MoreInformation { get; set; }

        public decimal? Rate { get; set; }
    }
}
