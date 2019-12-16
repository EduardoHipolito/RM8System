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
    public class SupplierModel : BaseModel
    {
        public int IdLegalPerson { get; set; }
        public LegalPersonModel FKLegalPerson { get; set; }
        public string FKLegalPersonName { get; set; }

        public string MoreInformation { get; set; }

        public SupplierType SupplierType { get; set; }
    }
}
