﻿// <auto-generated />
using System;
using CarRental.Respository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarRental.Respository.Migrations
{
    [DbContext(typeof(CarRentalContext))]
    [Migration("20240425074449_x6")]
    partial class x6
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
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
                        .HasMaxLength(20)
                        .HasColumnType("char")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDelted")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsReturn")
                        .HasColumnType("tinyint(1)");

                    b.Property<float>("Price")
                        .HasColumnType("float");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<byte?>("Status")
                        .HasColumnType("tinyint unsigned");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("BookingId");

                    b.HasIndex("CarId");

                    b.HasIndex("UserId");

                    b.ToTable("t_booking", null, t =>
                        {
                            t.HasComment("订单表");
                        });

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

                    b.Property<string>("CarType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("color");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("description");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("image");

                    b.Property<bool>("IsDelted")
                        .HasColumnType("tinyint(1)");

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

                    b.Property<byte?>("Status")
                        .HasColumnType("tinyint unsigned");

                    b.HasKey("CarId")
                        .HasName("PRIMARY");

                    b.HasIndex("ManagerId");

                    b.ToTable("t_car", null, t =>
                        {
                            t.HasComment("车辆表");
                        });
                });

            modelBuilder.Entity("CarRental.Respository.Models.Check", b =>
                {
                    b.Property<long>("CheckId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("CheckId"));

                    b.Property<long>("BookingId")
                        .HasColumnType("bigint");

                    b.Property<string>("CheckDesc")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CheckReference")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CheckTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDelted")
                        .HasColumnType("tinyint(1)");

                    b.Property<float>("PayMoney")
                        .HasColumnType("float");

                    b.Property<string>("Problem")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("CheckId");

                    b.HasIndex("BookingId")
                        .IsUnique();

                    b.ToTable("t_check", null, t =>
                        {
                            t.HasComment("检查单");
                        });

                    MySqlEntityTypeBuilderExtensions.UseCollation(b, "utf8mb4_general_ci");
                });

            modelBuilder.Entity("CarRental.Respository.Models.LogLogin", b =>
                {
                    b.Property<long>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("LogId"));

                    b.Property<bool>("IsDelted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LoginIp")
                        .HasColumnType("longtext");

                    b.Property<string>("LoginName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("LoginTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("LogId");

                    b.ToTable("t_log_login", null, t =>
                        {
                            t.HasComment("日志表");
                        });

                    MySqlEntityTypeBuilderExtensions.UseCollation(b, "utf8mb4_general_ci");
                });

            modelBuilder.Entity("CarRental.Respository.Models.Manager", b =>
                {
                    b.Property<long>("ManagerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("ManagerId"));

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<byte>("Available")
                        .HasColumnType("tinyint unsigned");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("varchar(18)")
                        .HasColumnName("email");

                    b.Property<bool>("IsDelted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)")
                        .HasColumnName("password");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ManagerId")
                        .HasName("PRIMARY");

                    b.ToTable("t_manager", null, t =>
                        {
                            t.HasComment("管理员表");
                        });

                    MySqlEntityTypeBuilderExtensions.UseCollation(b, "utf8mb4_general_ci");
                });

            modelBuilder.Entity("CarRental.Respository.Models.Menu", b =>
                {
                    b.Property<long>("MenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("MenuId"));

                    b.Property<byte>("Available")
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("IconPath")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasComment("菜单图标");

                    b.Property<long>("ParentId")
                        .HasColumnType("bigint");

                    b.Property<string>("Path")
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("MenuId");

                    b.ToTable("t_menu", null, t =>
                        {
                            t.HasComment("菜单表");
                        });

                    MySqlEntityTypeBuilderExtensions.UseCollation(b, "utf8mb4_general_ci");
                });

            modelBuilder.Entity("CarRental.Respository.Models.Role", b =>
                {
                    b.Property<long>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("RoleId"));

                    b.Property<byte>("Available")
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("Remarks")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("RoleId");

                    b.ToTable("t_role", null, t =>
                        {
                            t.HasComment("角色表");
                        });

                    MySqlEntityTypeBuilderExtensions.UseCollation(b, "utf8mb4_general_ci");
                });

            modelBuilder.Entity("CarRental.Respository.Models.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("UserId"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("varchar(18)")
                        .HasColumnName("email");

                    b.Property<string>("Identity")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDelted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

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

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("UserId");

                    b.ToTable("t_user", null, t =>
                        {
                            t.HasComment("用户表");
                        });

                    MySqlEntityTypeBuilderExtensions.UseCollation(b, "utf8mb4_general_ci");
                });

            modelBuilder.Entity("ManagerRole", b =>
                {
                    b.Property<long>("ManagersManagerId")
                        .HasColumnType("bigint");

                    b.Property<long>("RolesRoleId")
                        .HasColumnType("bigint");

                    b.HasKey("ManagersManagerId", "RolesRoleId");

                    b.HasIndex("RolesRoleId");

                    b.ToTable("t_role_manager", (string)null);
                });

            modelBuilder.Entity("MenuRole", b =>
                {
                    b.Property<long>("MenusMenuId")
                        .HasColumnType("bigint");

                    b.Property<long>("RolesRoleId")
                        .HasColumnType("bigint");

                    b.HasKey("MenusMenuId", "RolesRoleId");

                    b.HasIndex("RolesRoleId");

                    b.ToTable("t_menu_role", (string)null);
                });

            modelBuilder.Entity("CarRental.Respository.Models.Booking", b =>
                {
                    b.HasOne("CarRental.Respository.Models.Car", "Car")
                        .WithMany()
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

            modelBuilder.Entity("CarRental.Respository.Models.Check", b =>
                {
                    b.HasOne("CarRental.Respository.Models.Booking", "Booking")
                        .WithOne("Check")
                        .HasForeignKey("CarRental.Respository.Models.Check", "BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booking");
                });

            modelBuilder.Entity("ManagerRole", b =>
                {
                    b.HasOne("CarRental.Respository.Models.Manager", null)
                        .WithMany()
                        .HasForeignKey("ManagersManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarRental.Respository.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MenuRole", b =>
                {
                    b.HasOne("CarRental.Respository.Models.Menu", null)
                        .WithMany()
                        .HasForeignKey("MenusMenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarRental.Respository.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CarRental.Respository.Models.Booking", b =>
                {
                    b.Navigation("Check");
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
