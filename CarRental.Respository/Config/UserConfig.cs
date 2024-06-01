using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Repository.Entity
{
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .ToTable("t_user", tb => tb.HasComment("用户表"))
                .UseCollation("utf8mb4_general_ci");

            builder.Property(x => x.Password).HasMaxLength(150).HasColumnName("password");

            builder.Property(x => x.Email).HasMaxLength(18).HasColumnName("email");

            builder.Property(x => x.PhoneNumber).HasMaxLength(13).HasColumnName("phonenumber");

            builder.HasQueryFilter(x => x.IsDeleted == false);
        }
    }
}
