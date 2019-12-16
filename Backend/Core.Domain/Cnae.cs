using Framework.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Domain
{
    public class Cnae : EntityBase
    {
        [Required(ErrorMessage = "O código do CNAE é obrigatório")]
        public int Code { get; set; }

        [Required(ErrorMessage = "A descrição do CNAE é obrigatória")]
        [MaxLength(150)]
        [MinLength(4)]
        public string Description { get; set; }

        public ICollection<LegalPerson> LegalPeople { get; set; }

    }
}
