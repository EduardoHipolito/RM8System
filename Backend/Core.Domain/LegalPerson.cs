using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Domain
{
    public class LegalPerson : Person
    {
        [Required(ErrorMessage = "O nome fantasia é obrigatorio")]
        [MaxLength(100)]
        [MinLength(3)]
        public string FantasyName { get; set; }
        [Required(ErrorMessage = "A razão social é obrigatória")]
        [MaxLength(100)]
        [MinLength(5)]
        public string CorporateName { get; set; }

        [ForeignKey(nameof(FKCnae))]
        public int? IdCnae { get; set; }
        public Cnae FKCnae { get; set; }

        public ICollection<Company> Companies { get; set; }
    }
}
