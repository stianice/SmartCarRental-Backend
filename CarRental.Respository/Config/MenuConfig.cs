using CarRental.Respository.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Respository.Config
{
    internal class MenuConfig : IEntityTypeConfiguration<Menu>
    {
        void IEntityTypeConfiguration<Menu>.Configure(
            Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Menu> builder
        )
        {
            builder
                .ToTable("t_menu", tb => tb.HasComment("菜单表"))
                .UseCollation("utf8mb4_general_ci");

            builder
                .HasMany(x => x.Roles)
                .WithMany(x => x.Menus)
                .UsingEntity(x => x.ToTable("t_menu_role"));

            builder.Property(x => x.IconPath).HasMaxLength(20).HasComment("菜单图标");
        }
    }
}
