using Core.Domain.Enum;
using Framework.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Domain
{
    public class Phone : EntityBase
    {
        [Required(ErrorMessage = "O Tipo do telefone e obrigatoria")]
        [MaxLength(2)]
        [MinLength(1)]
        public PhoneType Type { get; set; }

        [Required(ErrorMessage = "O identificador da Pais e obrigatorio")]
        [ForeignKey(nameof(FKCountry))]
        public int IdCountry { get; set; }
        public Country FKCountry { get; set; }

        [Required(ErrorMessage = "A area de telefone e obrigatorio")]
        public int AreaCode { get; set; }

        [Required(ErrorMessage = "O numero de telefone e obrigatorio")]
        public int Number { get; set; }

        [Required(ErrorMessage = "O identificador da pessoa e obrigatorio")]
        [ForeignKey(nameof(FKPerson))]
        public int IdPerson { get; set; }
        public Person FKPerson { get; set; }

    }
}
