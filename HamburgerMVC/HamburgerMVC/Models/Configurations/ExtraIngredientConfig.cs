using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HamburgerMVC.Models.Configurations
{
    public class ExtraIngredientConfig : IEntityTypeConfiguration<ExtraIngredient>
    {
        public void Configure(EntityTypeBuilder<ExtraIngredient> builder)
        {
            builder.HasOne(x => x.Order).WithMany(x => x.ExtraIngredients).HasForeignKey(x => x.OrderId);
        }
    }
}
