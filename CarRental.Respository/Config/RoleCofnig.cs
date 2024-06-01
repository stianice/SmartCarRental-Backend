using CarRental.Repository.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Repository.Config
{
    internal class RoleCofnig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder
                .ToTable("t_role", tb => tb.HasComment("角色表"))
                .UseCollation("utf8mb4_general_ci");

            builder.HasMany(x => x.Menus).WithMany().UsingEntity(x => x.ToTable("t_menu_role"));
        }
    }
}
