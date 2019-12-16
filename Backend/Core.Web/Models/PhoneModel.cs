using AutoMapper;
using Core.Domain.Enum;
using Framework.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Web.Models
{
    public class PhoneModel : BaseModel
    {
        public PhoneType? Type { get; set; }
        public int? IdCountry { get; set; }
        public int? AreaCode { get; set; }
        public int? Number { get; set; }
        public int? IdPerson { get; set; }
    }
}
