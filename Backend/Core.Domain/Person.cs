using Framework.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Domain
{
    public class Person : EntityBase
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(60)]
        [MinLength(10)]
        public string Name { get; set; }


        [Required(ErrorMessage = "O Endereco de email é obrigatorio")]
        [MaxLength(130)]
        [MinLength(6)]
        public string Email { get; set; }

        [MaxLength(80)]
        public string HomePage { get; set; }

        public ICollection<Document> Documents { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public ICollection<Phone> Phones { get; set; }

    }
}
