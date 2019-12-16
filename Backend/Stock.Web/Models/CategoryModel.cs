using Framework.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stock.Web.Models
{
    public class CategoryModel : BaseModel
    {
        [MaxLength(60)]
        [MinLength(4)]
        public string Name { get; set; }

        [MaxLength(130)]
        public string Description { get; set; }

    }
}
