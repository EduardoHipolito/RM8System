using Core.Domain.Enum;
using Framework.Domain;
using Framework.Domain.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Domain
{
    public class UserAplicationCompany : EntityBase
    {
        [Required(ErrorMessage = "O id da aplicacao é obrigatório")]
        [ForeignKey(nameof(FKAplication))]
        public int IdAplication { get; set; }
        [Filter(true)]
        public Aplication FKAplication { get; set; }
        [NotMapped]
        [Filter("FKAplication.Name")]
        public string FKAplicationName { get { return FKAplication != null ? FKAplication.Name : ""; } }

        [ForeignKey(nameof(FKUser))]
        public int? IdUser { get; set; }
        [Filter(true)]
        public User FKUser { get; set; }
        [NotMapped]
        [Filter("FKUser.FKPerson.Name")]
        public string FKUserName { get { return FKUser != null ? FKUser.FKPerson != null ? FKUser.FKPerson.Name : "" : ""; } }

        [ForeignKey(nameof(FKCompany))]
        public int? IdCompany { get; set; }
        [Filter(true)]
        public Company FKCompany { get; set; }
        [NotMapped]
        [Filter("FKCompany.ReducedName")]
        public string FKCompanyName { get { return FKCompany != null ? FKCompany.ReducedName : ""; } }

        [Required(ErrorMessage = "O Nivel de Acesso é obrigatório")]
        public AccessLevel AccessLevel { get; set; }

        public bool IsGlobal { get; set; } = false;
    }
}