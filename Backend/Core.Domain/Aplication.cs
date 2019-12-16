using Framework.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain
{
    public class Aplication : EntityBase
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(60, ErrorMessage = "O nome deve ter no maximo 60 caracteres")]
        [MinLength(5, ErrorMessage = "O nome deve ter no minimo 8 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [MaxLength(250, ErrorMessage = "A descrição deve ter no maximo 250 caracteres")]
        [MinLength(15, ErrorMessage = "A descrição deve ter no minimo 15 caracteres")]
        public string Description { get; set; }

        [Required(ErrorMessage = "O link é obrigatório")]
        [MaxLength(100, ErrorMessage = "O link deve ter no maximo 250 caracteres")]
        [MinLength(3, ErrorMessage = "O link deve ter no minimo 15 caracteres")]
        public string Link { get; set; }

        [Required(ErrorMessage = "O indice é obrigatório")]
        public int Index { get; set; }

        [ForeignKey("FKModule")]
        [Required(ErrorMessage = "A aplicação deve ter um modulo")]
        public int IdModule { get; set; }
        public Module FKModule { get; set; }
        public ICollection<UserAplicationCompany> Permitions { get; set; }

        [Required(ErrorMessage = "O codigo da aplicação é obrigatório")]
        public string AplicationCode { get; set; }

        public bool ShowMenu { get; set; } = true;
    }
}
