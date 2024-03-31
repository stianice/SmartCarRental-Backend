using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Respository.Models
{
    internal class ManagerConfig : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder.
                ToTable("t_manager", tb => tb.HasComment("管理员表")).
                HasCharSet("utf8mb4").
                UseCollation("utf8mb4_general_ci");

            //设置主键
            builder.HasKey(x => x.Id).
                    HasName("PRIMARY");

            builder.Property(x => x.address).
                    HasColumnName("address");

            builder.Property(x => x.Fname).
                    HasMaxLength(15).
                    HasColumnName("fname");
            
            builder.Property(x => x.Lname).
                    HasMaxLength(15).
                    HasColumnName("lname");
            
            builder.Property(x => x.Password).
                    HasMaxLength(18).
                    HasColumnName("password"); 
            
            builder.Property(x => x.Email).
                    HasMaxLength(18).
                    HasColumnName("email");

            builder.Property(x => x.Blance).
                   HasColumnName("blance");

            builder.HasMany(x => x.Cars).WithOne(x => x.Manager).IsRequired();

        }
    }
}
