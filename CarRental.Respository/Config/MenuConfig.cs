using CarRental.Repository.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CarRental.Repository.Config
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

            builder.Property(x => x.IconPath).HasMaxLength(20).HasComment("菜单图标");

            builder
                .HasMany(x => x.Children)
                .WithOne()
                .HasForeignKey(x => x.ParentId)
                .IsRequired(false);
        }
    }
}
