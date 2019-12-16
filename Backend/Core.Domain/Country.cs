using Framework.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Domain
{
    public class Country: EntityBase
    {
        public Country():base()
        {
        }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(60, ErrorMessage = "O nome deve ter no maximo 60 caracteres")]
        [MinLength(5, ErrorMessage = "O nome deve ter no minimo 4 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A Sigla é obrigatório")]
        [MaxLength(5, ErrorMessage = "A Sigla deve ter no maximo 60 caracteres")]
        [MinLength(2, ErrorMessage = "A Sigla deve ter no minimo 4 caracteres")]
        public string Code { get; set; }

        [Required(ErrorMessage = "O Codigo telefone é obrigatório")]
        public int PhoneCode { get; set; }

        public ICollection<City> Cities { get; set; }
        public ICollection<State> States { get; set; }
    
    }
}
