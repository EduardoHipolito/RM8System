using Framework.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Domain
{
    public class Document : EntityBase
    {

        [Required(ErrorMessage = "O tipo do documento é obrigatório")]
        [ForeignKey(nameof(FKDocumentType))]
        public int IdDocumentType { get; set; }
        public DocumentType FKDocumentType { get; set; }

        [Required(ErrorMessage = "O documento é obrigatório")]
        [MaxLength(30)]
        public string Value { get; set; }

        [Required(ErrorMessage = "O Id da pessoa é obrigatório")]
        [ForeignKey(nameof(FKPerson))]
        public int IdPerson { get; set; }
        public Person FKPerson { get; set; }

    }
}
