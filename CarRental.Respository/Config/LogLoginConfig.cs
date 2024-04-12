using CarRental.Respository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Respository.Config
{
    internal class LogLoginConfig : IEntityTypeConfiguration<LogLogin>
    {
        public void Configure(EntityTypeBuilder<LogLogin> builder)
        {
            builder.Property<long>("LogId");

            builder
                .ToTable("t_log_login", tb => tb.HasComment("日志表"))
                .UseCollation("utf8mb4_general_ci")
                .HasKey("LogId");
            builder.HasQueryFilter(x => x.IsDelted == false);
        }
    }
}
