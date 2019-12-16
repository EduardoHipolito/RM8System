using Framework.Domain;
using Framework.Domain.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Domain
{
    public class City : EntityBase
    {

        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(60, ErrorMessage = "O nome deve ter no maximo 60 caracteres")]
        [MinLength(5, ErrorMessage = "O nome deve ter no minimo 4 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O identificador da Pais e obrigatorio")]
        [ForeignKey(nameof(FKCountry))]
        public int IdCountry { get; set; }
        [Filter(true)]
        public Country FKCountry { get; set; }
        [NotMapped]
        [Filter("FKCountry.Name")]
        public string FKCountryName { get { return FKCountry != null ? FKCountry.Name : ""; } }

        [Required(ErrorMessage = "O identificador do Estado e obrigatorio")]
        [ForeignKey(nameof(FKState))]
        public int? IdState { get; set; }
        [Filter(true)]
        public State FKState { get; set; }
        [NotMapped]
        [Filter("FKState.Name")]
        public string FKStateName { get { return FKState != null ? FKState.Name : ""; } }
    }
}
