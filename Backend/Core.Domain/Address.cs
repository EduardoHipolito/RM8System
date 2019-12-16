using Core.Domain.Enum;
using Framework.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Domain
{
    public class Address : EntityBase
    {
        [ForeignKey(nameof(FKPerson))]
        public int IdPerson { get; set; }
        public Person FKPerson { get; private set; }

        [Required(ErrorMessage = "O Tipo do endereco e obrigatorio")]
        public AddressType Type { get; set; }

        [Required(ErrorMessage = "O Tipo de Logradouro e obrigatorio")]
        public PublicAreaType PublicAreaType { get; set; }

        [Required(ErrorMessage = "O Logradouro do endereço é obrigatorio")]
        [MaxLength(130)]
        [MinLength(6)]
        public string PublicArea { get; set; }
        [MaxLength(20)]
        public string Complement { get; set; }

        [Required(ErrorMessage = "O Numero do endereço é obrigatorio")]
        public int Number { get; set; }

        [Required(ErrorMessage = "O Bairro do endereço é obrigatorio")]
        [MaxLength(50)]
        [MinLength(6)]
        public string Neighborhood { get; set; }

        [Required(ErrorMessage = "O identificador da Pais e obrigatorio")]
        [ForeignKey(nameof(FKCountry))]
        public int IdCountry { get; set; }
        public Country FKCountry { get; set; }

        [ForeignKey(nameof(FKState))]
        public int? IdState { get; set; }
        public State FKState { get; set; }

        [ForeignKey(nameof(FKCity))]
        public int? IdCity { get; set; }
        public City FKCity { get; set; }

        public Int64? PostalCode { get; set; }

    }
}
