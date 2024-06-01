using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Repository.Entity
{
    internal class ManagerConfig : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder
                .ToTable("t_manager", tb => tb.HasComment("管理员表"))
                .UseCollation("utf8mb4_general_ci");

            //设置主键
            builder.HasKey(x => x.ManagerId).HasName("PRIMARY");

            builder.Property(x => x.Password).HasMaxLength(150).HasColumnName("password");

            builder.Property(x => x.Email).HasMaxLength(18).HasColumnName("email");

            //builder.Property(x => x.Balance).HasColumnName("balance");

            builder.HasMany(x => x.Cars).WithOne(x => x.Manager).IsRequired();

            builder
                .HasMany(x => x.Roles)
                .WithMany(x => x.Managers)
                .UsingEntity(x => x.ToTable("t_role_manager"));

            //约定发现一对多car和manager的关系
        }
    }
}
