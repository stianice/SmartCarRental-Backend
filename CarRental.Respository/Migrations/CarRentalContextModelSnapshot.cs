﻿// <auto-generated />
using System;
using CarRental.Repository;
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
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("CarRental.Repository.Entity.Booking", b =>
                {
                    b.Property<long>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("booking_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("BookingId"));

                    b.Property<string>("BookingReference")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("booking_reference");

                    b.Property<long>("CarId")
                        .HasColumnType("bigint")
                        .HasColumnName("car_id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("char")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_date");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("end_date");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_deleted");

                    b.Property<float>("Price")
                        .HasColumnType("float")
                        .HasColumnName("price");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("start_date");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint unsigned")
                        .HasColumnName("status");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("BookingId")
                        .HasName("pk_t_booking");

                    b.HasIndex("CarId")
                        .HasDatabaseName("ix_t_booking_car_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_t_booking_user_id");

                    b.ToTable("t_booking", null, t =>
                        {
                            t.HasComment("订单表");
                        });

                    MySqlEntityTypeBuilderExtensions.UseCollation(b, "utf8mb4_general_ci");
                });

            modelBuilder.Entity("CarRental.Repository.Entity.Car", b =>
                {
                    b.Property<long>("CarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("car_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("CarId"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("brand");

                    b.Property<string>("CarType")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("car_type");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("color");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("description");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("image");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_deleted");

                    b.Property<long>("ManagerId")
                        .HasColumnType("bigint")
                        .HasColumnName("manager_id");

                    b.Property<float>("Price")
                        .HasColumnType("float")
                        .HasColumnName("price");

                    b.Property<string>("Registration")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("registration");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint unsigned")
                        .HasColumnName("status");

                    b.HasKey("CarId")
                        .HasName("PRIMARY");

                    b.HasIndex("ManagerId")
                        .HasDatabaseName("ix_t_car_manager_id");

                    b.ToTable("t_car", null, t =>
                        {
                            t.HasComment("车辆表");
                        });
                });

            modelBuilder.Entity("CarRental.Repository.Entity.Check", b =>
                {
                    b.Property<long>("CheckId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("check_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("CheckId"));

                    b.Property<long>("BookingId")
                        .HasColumnType("bigint")
                        .HasColumnName("booking_id");

                    b.Property<string>("CheckDesc")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("check_desc");

                    b.Property<string>("CheckReference")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("check_reference");

                    b.Property<DateTime>("CheckTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("check_time");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_date");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_deleted");

                    b.Property<float>("PayMoney")
                        .HasColumnType("float")
                        .HasColumnName("pay_money");

                    b.Property<string>("Problem")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("problem");

                    b.HasKey("CheckId")
                        .HasName("pk_t_check");

                    b.HasIndex("BookingId")
                        .IsUnique()
                        .HasDatabaseName("ix_t_check_booking_id");

                    b.ToTable("t_check", null, t =>
                        {
                            t.HasComment("检查单");
                        });

                    MySqlEntityTypeBuilderExtensions.UseCollation(b, "utf8mb4_general_ci");
                });

            modelBuilder.Entity("CarRental.Repository.Entity.LogLogin", b =>
                {
                    b.Property<long>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("log_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("LogId"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_deleted");

                    b.Property<string>("LoginIp")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("login_ip");

                    b.Property<string>("LoginName")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("login_name");

                    b.Property<DateTime>("LoginTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("login_time");

                    b.HasKey("LogId")
                        .HasName("pk_t_log_login");

                    b.ToTable("t_log_login", null, t =>
                        {
                            t.HasComment("日志表");
                        });

                    MySqlEntityTypeBuilderExtensions.UseCollation(b, "utf8mb4_general_ci");
                });

            modelBuilder.Entity("CarRental.Repository.Entity.Manager", b =>
                {
                    b.Property<long>("ManagerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("manager_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("ManagerId"));

                    b.Property<string>("Address")
                        .HasColumnType("longtext")
                        .HasColumnName("address");

                    b.Property<bool>("Available")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("available");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("varchar(18)")
                        .HasColumnName("email");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)")
                        .HasColumnName("password");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("position");

                    b.HasKey("ManagerId")
                        .HasName("PRIMARY");

                    b.ToTable("t_manager", null, t =>
                        {
                            t.HasComment("管理员表");
                        });

                    MySqlEntityTypeBuilderExtensions.UseCollation(b, "utf8mb4_general_ci");
                });

            modelBuilder.Entity("CarRental.Repository.Entity.Menu", b =>
                {
                    b.Property<long>("MenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("menu_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("MenuId"));

                    b.Property<short>("Available")
                        .HasColumnType("smallint")
                        .HasColumnName("available");

                    b.Property<string>("IconPath")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("icon_path")
                        .HasComment("菜单图标");

                    b.Property<byte>("IsDeleted")
                        .HasColumnType("tinyint unsigned")
                        .HasColumnName("is_deleted");

                    b.Property<byte>("Level")
                        .HasColumnType("tinyint unsigned")
                        .HasColumnName("level");

                    b.Property<long?>("ParentId")
                        .HasColumnType("bigint")
                        .HasColumnName("parent_id");

                    b.Property<string>("Path")
                        .HasColumnType("longtext")
                        .HasColumnName("path");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("title");

                    b.HasKey("MenuId")
                        .HasName("pk_t_menu");

                    b.HasIndex("ParentId")
                        .HasDatabaseName("ix_t_menu_parent_id");

                    b.ToTable("t_menu", null, t =>
                        {
                            t.HasComment("菜单表");
                        });

                    MySqlEntityTypeBuilderExtensions.UseCollation(b, "utf8mb4_general_ci");
                });

            modelBuilder.Entity("CarRental.Repository.Entity.Role", b =>
                {
                    b.Property<long>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("role_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("RoleId"));

                    b.Property<byte>("Available")
                        .HasColumnType("tinyint unsigned")
                        .HasColumnName("available");

                    b.Property<byte>("Deleted")
                        .HasColumnType("tinyint unsigned")
                        .HasColumnName("deleted");

                    b.Property<string>("Remarks")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("remarks");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("role_name");

                    b.HasKey("RoleId")
                        .HasName("pk_t_role");

                    b.ToTable("t_role", null, t =>
                        {
                            t.HasComment("角色表");
                        });

                    MySqlEntityTypeBuilderExtensions.UseCollation(b, "utf8mb4_general_ci");
                });

            modelBuilder.Entity("CarRental.Repository.Entity.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("UserId"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("city");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("varchar(18)")
                        .HasColumnName("email");

                    b.Property<string>("Identity")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("identity");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("name");

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
                        .HasColumnType("longtext")
                        .HasColumnName("sex");

                    b.HasKey("UserId")
                        .HasName("pk_t_user");

                    b.ToTable("t_user", null, t =>
                        {
                            t.HasComment("用户表");
                        });

                    MySqlEntityTypeBuilderExtensions.UseCollation(b, "utf8mb4_general_ci");
                });

            modelBuilder.Entity("ManagerRole", b =>
                {
                    b.Property<long>("ManagersManagerId")
                        .HasColumnType("bigint")
                        .HasColumnName("managers_manager_id");

                    b.Property<long>("RolesRoleId")
                        .HasColumnType("bigint")
                        .HasColumnName("roles_role_id");

                    b.HasKey("ManagersManagerId", "RolesRoleId")
                        .HasName("pk_t_role_manager");

                    b.HasIndex("RolesRoleId")
                        .HasDatabaseName("ix_t_role_manager_roles_role_id");

                    b.ToTable("t_role_manager", (string)null);
                });

            modelBuilder.Entity("MenuRole", b =>
                {
                    b.Property<long>("MenusMenuId")
                        .HasColumnType("bigint")
                        .HasColumnName("menus_menu_id");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint")
                        .HasColumnName("role_id");

                    b.HasKey("MenusMenuId", "RoleId")
                        .HasName("pk_t_menu_role");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_t_menu_role_role_id");

                    b.ToTable("t_menu_role", (string)null);
                });

            modelBuilder.Entity("CarRental.Repository.Entity.Booking", b =>
                {
                    b.HasOne("CarRental.Repository.Entity.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_t_booking_cars_car_id");

                    b.HasOne("CarRental.Repository.Entity.User", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_t_booking_users_user_id");

                    b.Navigation("Car");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CarRental.Repository.Entity.Car", b =>
                {
                    b.HasOne("CarRental.Repository.Entity.Manager", "Manager")
                        .WithMany("Cars")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_t_car_t_manager_manager_id");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("CarRental.Repository.Entity.Check", b =>
                {
                    b.HasOne("CarRental.Repository.Entity.Booking", "Booking")
                        .WithOne()
                        .HasForeignKey("CarRental.Repository.Entity.Check", "BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_t_check_bookings_booking_id");

                    b.Navigation("Booking");
                });

            modelBuilder.Entity("CarRental.Repository.Entity.Menu", b =>
                {
                    b.HasOne("CarRental.Repository.Entity.Menu", null)
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .HasConstraintName("fk_t_menu_t_menu_parent_id");
                });

            modelBuilder.Entity("ManagerRole", b =>
                {
                    b.HasOne("CarRental.Repository.Entity.Manager", null)
                        .WithMany()
                        .HasForeignKey("ManagersManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_t_role_manager_t_manager_managers_manager_id");

                    b.HasOne("CarRental.Repository.Entity.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_t_role_manager_t_role_roles_role_id");
                });

            modelBuilder.Entity("MenuRole", b =>
                {
                    b.HasOne("CarRental.Repository.Entity.Menu", null)
                        .WithMany()
                        .HasForeignKey("MenusMenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_t_menu_role_t_menu_menus_menu_id");

                    b.HasOne("CarRental.Repository.Entity.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_t_menu_role_t_role_role_id");
                });

            modelBuilder.Entity("CarRental.Repository.Entity.Manager", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("CarRental.Repository.Entity.Menu", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("CarRental.Repository.Entity.User", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
