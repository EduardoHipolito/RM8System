using Framework.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Models
{
    public class BaseModel
    {
        public int? Id { get; set; } = 0;
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public EntityType Status { get; set; } = EntityType.Active;
        public int? IdCompanyPermition { get; set; }
    }
}
