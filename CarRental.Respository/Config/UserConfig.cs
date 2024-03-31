using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Respository.Models
{
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("t_user", tb => tb.HasComment("用户表")).
                    HasCharSet("utf8mb4").
                    UseCollation("utf8mb4_general_ci").
                    HasKey(x => x.Id).HasName("PRIMARY");

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
            
            builder.Property(x => x.Phone).
                    HasMaxLength(13).
                    HasColumnName("phone"); 
            
            builder.Property(x => x.Blance).
                    HasColumnName("blance");

            builder.HasMany(x => x.Bookings).WithOne(x => x.User).IsRequired();
        }
    }
}
