using Framework.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stock.Web.Models
{
    public class CustomerModel : BaseModel
    {
        public int IdPhysicalPerson { get; set; }
        public PhysicalPersonModel FKPhysicalPerson { get; set; }

        public string FKPhysicalPersonName { get; set; }
        public string MoreInformation { get; set; }

        public decimal Limit { get; set; }
    }
}
