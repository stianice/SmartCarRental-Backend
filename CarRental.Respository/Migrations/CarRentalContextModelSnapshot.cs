﻿// <auto-generated />
using System;
using CarRental.Respository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarRental.Respository.Migrations
{
    [DbContext(typeof(CarRentalContext))]
    partial class CarRentalContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("CarRental.Respository.Models.Booking", b =>
                {
                    b.Property<long>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("BookingId"));

                    b.Property<string>("BookingReference")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("booking_reference");

                    b.Property<long>("CarId")
                        .HasColumnType("bigint");

                    b.Property<string>("Content")
                        .HasColumnType("char")
                        .HasColumnName("content");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("status");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("BookingId")
                        .HasName("PRIMARY");

                    b.HasIndex("CarId");

                    b.HasIndex("UserId");

                    b.ToTable("t_booking", null, t =>
                        {
                            t.HasComment("订单表");
                        });

                    MySqlEntityTypeBuilderExtensions.HasCharSet(b, "utf8mb4");
                    MySqlEntityTypeBuilderExtensions.UseCollation(b, "utf8mb4_general_ci");
                });

            modelBuilder.Entity("CarRental.Respository.Models.Car", b =>
                {
                    b.Property<long>("CarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("CarId"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("brand");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("color");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("description");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("image");

                    b.Property<long>("ManagerId")
                        .HasColumnType("bigint");

                    b.Property<float>("Price")
                        .HasColumnType("float")
                        .HasColumnName("price");

                    b.Property<string>("Registration")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("registration");

                    b.HasKey("CarId")
                        .HasName("PRIMARY");

                    b.HasIndex("ManagerId");

                    b.ToTable("t_car", null, t =>
                        {
                            t.HasComment("车辆表");
                        });
                });

            modelBuilder.Entity("CarRental.Respository.Models.Manager", b =>
                {
                    b.Property<long>("ManagerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("ManagerId"));

                    b.Property<string>("Address")
                        .HasColumnType("longtext")
                        .HasColumnName("address");

                    b.Property<float>("Balance")
                        .HasColumnType("float")
                        .HasColumnName("balance");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("varchar(18)")
                        .HasColumnName("email");

                    b.Property<string>("Fname")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("fname");

                    b.Property<string>("Lname")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("lname");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)")
                        .HasColumnName("password");

                    b.HasKey("ManagerId")
                        .HasName("PRIMARY");

                    b.ToTable("t_manager", null, t =>
                        {
                            t.HasComment("管理员表");
                        });

                    MySqlEntityTypeBuilderExtensions.HasCharSet(b, "utf8mb4");
                    MySqlEntityTypeBuilderExtensions.UseCollation(b, "utf8mb4_general_ci");
                });

            modelBuilder.Entity("CarRental.Respository.Models.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("UserId"));

                    b.Property<float>("Balance")
                        .HasColumnType("float")
                        .HasColumnName("balance");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("varchar(18)")
                        .HasColumnName("email");

                    b.Property<string>("Fname")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("fname");

                    b.Property<string>("Lname")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("lname");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)")
                        .HasColumnName("password");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("varchar(13)")
                        .HasColumnName("phonenumber");

                    b.HasKey("UserId")
                        .HasName("PRIMARY");

                    b.ToTable("t_user", null, t =>
                        {
                            t.HasComment("用户表");
                        });

                    MySqlEntityTypeBuilderExtensions.HasCharSet(b, "utf8mb4");
                    MySqlEntityTypeBuilderExtensions.UseCollation(b, "utf8mb4_general_ci");
                });

            modelBuilder.Entity("CarRental.Respository.Models.Booking", b =>
                {
                    b.HasOne("CarRental.Respository.Models.Car", "Car")
                        .WithMany("Bookings")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarRental.Respository.Models.User", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CarRental.Respository.Models.Car", b =>
                {
                    b.HasOne("CarRental.Respository.Models.Manager", "Manager")
                        .WithMany("Cars")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("CarRental.Respository.Models.Car", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("CarRental.Respository.Models.Manager", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("CarRental.Respository.Models.User", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
