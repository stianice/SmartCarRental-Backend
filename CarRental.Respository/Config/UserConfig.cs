﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Respository.Models
{
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .ToTable("t_user", tb => tb.HasComment("用户表"))
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci")
                .HasKey(x => x.UserId)
                .HasName("PRIMARY");

            builder.Property(x => x.Fname).HasMaxLength(15).HasColumnName("fname");

            builder.Property(x => x.Lname).HasMaxLength(15).HasColumnName("lname");

            builder.Property(x => x.Password).HasMaxLength(150).HasColumnName("password");

            builder.Property(x => x.Email).HasMaxLength(18).HasColumnName("email");

            builder.Property(x => x.PhoneNumber).HasMaxLength(13).HasColumnName("phonenumber");

            builder.Property(x => x.Balance).HasColumnName("balance");

            builder.HasMany(x => x.Bookings).WithOne(x => x.User).IsRequired();
        }
    }
}
