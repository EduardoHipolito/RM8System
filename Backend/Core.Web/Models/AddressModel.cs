using AutoMapper;
using Core.Domain.Enum;
using Framework.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Web.Models
{
    public class AddressModel : BaseModel
    {
        public int? IdPerson { get; set; }
        public AddressType? Type { get; set; }
        public PublicAreaType? PublicAreaType { get; set; }
        public string PublicArea { get; set; }
        public string Complement { get; set; }
        public int? Number { get; set; }
        public string Neighborhood { get; set; }
        public int? IdCountry { get; set; }
        public int? IdState { get; set; }
        public int? IdCity { get; set; }

        public Int64? PostalCode { get; set; }
    }
}
