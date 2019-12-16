using Core.Domain;
using Core.Domain.Enum;
using Framework.Domain;
using Framework.Domain.Attributes;
using Stock.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stock.Domain
{
    public class Supplier : EntityBase
    {
        [Required(ErrorMessage = "A pessoa é obrigatoria")]
        public int IdLegalPerson { get; set; }
        [NotMapped]
        [Filter(true)]
        public LegalPerson FKLegalPerson { get; set; }
        [Filter("FKLegalPerson.Name")]
        public string FKLegalPersonName { get { return FKLegalPerson != null ? FKLegalPerson.Name : ""; } }

        public string MoreInformation { get; set; }

        public SupplierType SupplierType { get; set; }
    }
}
