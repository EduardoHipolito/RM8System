using AutoMapper;
using Framework.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.Web.Models
{
    public class PhysicalPersonModel : BaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string HomePage { get; set; }
    }
}
