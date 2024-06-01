using CarRental.Repository.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Repository.Config
{
    internal class CheckConfig : IEntityTypeConfiguration<Check>
    {
        void IEntityTypeConfiguration<Check>.Configure(EntityTypeBuilder<Check> builder)
        {
            builder
                .ToTable("t_check", tb => tb.HasComment("检查单"))
                .UseCollation("utf8mb4_general_ci");

            builder.HasOne(x => x.Booking).WithOne().HasForeignKey<Check>(x => x.BookingId);

            builder.HasQueryFilter(x => x.IsDeleted == false);
        }
    }
}
