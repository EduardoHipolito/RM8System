using Framework.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Domain
{
    public class Module: EntityBase
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(60, ErrorMessage = "O nome deve ter no maximo 60 caracteres")]
        [MinLength(5, ErrorMessage = "O nome deve ter no minimo 8 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [MaxLength(250, ErrorMessage = "A descrição deve ter no maximo 250 caracteres")]
        [MinLength(15, ErrorMessage = "A descrição deve ter no minimo 15 caracteres")]
        public string Description { get; set; }

        [Required(ErrorMessage = "O indice é obrigatório")]
        public int Index { get; set; }

        [Required(ErrorMessage = "O indice é obrigatório")]
        public string Icon { get; set; }

        public ICollection<Aplication> Aplications { get; set; }

        [Required(ErrorMessage = "O codigo do modulo é obrigatório")]
        public string ModuleCode { get; set; }
    }
}
