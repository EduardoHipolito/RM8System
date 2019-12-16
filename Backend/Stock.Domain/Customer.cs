using Core.Domain;
using Core.Domain.Enum;
using Framework.Domain;
using Framework.Domain.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stock.Domain
{
    public class Customer : EntityBase
    {
        [Required(ErrorMessage = "A pessoa é obrigatoria")]
        public int IdPhysicalPerson { get; set; }

        [NotMapped]
        [Filter(true)]
        public PhysicalPerson FKPhysicalPerson { get; set; }
        [Filter("FKPhysicalPerson.Name")]
        public string FKPhysicalPersonName { get { return FKPhysicalPerson != null ? FKPhysicalPerson.Name : ""; } }

        public string MoreInformation { get; set; }

        public decimal Limit { get; set; }
    }
}
