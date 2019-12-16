using Framework.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stock.Domain
{
    public class Category : EntityBase
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(60)]
        [MinLength(4)]
        public string Name { get; set; }

        [MaxLength(130)]
        public string Description { get; set; }

    }
}
