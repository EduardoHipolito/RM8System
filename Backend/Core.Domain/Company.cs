using Core.Domain.Enum;
using Framework.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Domain
{
    public class Company : EntityBase
    {
        [Required(ErrorMessage = "O tipo da Loja deve ser matriz ou filial")]
        public CompanyType Type { get; set; }

        [Required(ErrorMessage = "O dia de vencimento é obrigatório")]
        public int PaymentDay { get; set; }

        [ForeignKey(nameof(FKPerson))]
        public int IdPerson { get; set; }
        public LegalPerson FKPerson { get; private set; }

        [ForeignKey(nameof(FKMaster))]
        public int? IdMaster { get; set; }
        public Company FKMaster { get; set; }

        [Required(ErrorMessage = "O nome reduzido é obrigatorio")]
        [MaxLength(40)]
        [MinLength(3)]
        public string ReducedName { get; set; }

        public ICollection<UserAplicationCompany> Permitions { get; set; }
        public ICollection<Company> Children { get; set; }
    }
}
