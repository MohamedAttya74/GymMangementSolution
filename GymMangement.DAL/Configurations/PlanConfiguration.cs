using GymMangement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymMangement.Configurations
{
    public class PlanConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.Property(x=>x.Name)
                    .HasColumnType("varchar")
                    .HasMaxLength(50);


            builder.Property(x => x.Description)
                    .HasMaxLength(200);

            builder.Property(x => x.Price)
                    .HasPrecision(10, 2);

            builder.Property(X => X.CreatedAt)
                    .HasDefaultValueSql("GETDATE()");
            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("PlanDurationCheck", "DurationDays Between 1 And 365");
            });
        }
    }
}
