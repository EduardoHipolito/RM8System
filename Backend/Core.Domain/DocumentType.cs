using Core.Domain.Enum;
using Framework.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Domain
{
    public class DocumentType : EntityBase
    {

        [Required(ErrorMessage = "O nome do tipo de documento é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O tipo de pessoa é obrigatório")]
        [MaxLength(30)]
        public PersonType PersonType { get; set; }

        [Required(ErrorMessage = "O Id do país é obrigatório")]
        [ForeignKey(nameof(FKCountry))]
        public int IdCountry { get; set; }
        public Country FKCountry { get; set; }

    }
}
