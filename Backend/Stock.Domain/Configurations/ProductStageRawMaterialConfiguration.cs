using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Stock.Domain.Configurations
{
    public class ProductionStageConfiguration
    {
        public ProductionStageConfiguration(EntityTypeBuilder<ProductionStage> entity)
        {
        }
    }
}
