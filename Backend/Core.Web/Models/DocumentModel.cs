using AutoMapper;
using Framework.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Web.Models
{
    public class DocumentModel : BaseModel
    {
        public int? IdDocumentType { get; set; }
        public string Value { get; set; }
        public int? IdPerson { get; set; }
    }
}
