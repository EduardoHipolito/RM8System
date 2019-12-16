using Framework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Domain.Configurations
{
    public class DocumentConfiguration
    {
        public DocumentConfiguration(EntityTypeBuilder<Document> entity)
        {
            entity.HasIndex(x => new { x.Value, x.IdDocumentType}).IsUnique().HasName("IX_UniqueDocument");
        }
    }
}

