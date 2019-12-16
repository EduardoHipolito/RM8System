using Framework.Domain.Attributes;
using Framework.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Framework.Domain
{

    public abstract class EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Filter(true)]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        [Required(ErrorMessage = "O status é obrigatório")]
        [DefaultValue(EntityType.Active)]
        public EntityType Status { get; set; }
        public int? IdCompanyPermition { get; set; }
        [NotMapped]
        public bool IsNull { get; set; }

        public EntityBase()
        {
            CreateDate = DateTime.Now;
            Status = EntityType.Active;
        }
    }
}
