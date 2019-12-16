using Framework.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Web.Models
{
    public class PersonModel : BaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string HomePage { get; set; }
        public List<DocumentModel> Documents { get; set; }
        public List<AddressModel> Addresses { get; set; }
        public List<PhoneModel> Phones { get; set; }
    }
}
