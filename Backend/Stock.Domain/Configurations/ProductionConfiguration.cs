using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Stock.Domain.Configurations
{
    public class ProductionConfiguration
    {
        public ProductionConfiguration(EntityTypeBuilder<Production> entity)
        {
        }
    }
}
