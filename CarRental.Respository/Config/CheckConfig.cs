using CarRental.Respository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Respository.Config
{
    internal class CheckConfig : IEntityTypeConfiguration<Check>
    {
        void IEntityTypeConfiguration<Check>.Configure(EntityTypeBuilder<Check> builder)
        {
            builder
                .ToTable("t_check", tb => tb.HasComment("检查单"))
                .UseCollation("utf8mb4_general_ci");

            builder.HasQueryFilter(x => x.IsDelted == false);
        }
    }
}
