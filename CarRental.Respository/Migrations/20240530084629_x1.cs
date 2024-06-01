using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Respository.Migrations
{
    /// <inheritdoc />
    public partial class x1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_log_login",
                columns: table => new
                {
                    log_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    login_name = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    login_ip = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    login_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_t_log_login", x => x.log_id);
                },
                comment: "日志表")
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "t_manager",
                columns: table => new
                {
                    manager_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    email = table.Column<string>(type: "varchar(18)", maxLength: 18, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    position = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    available = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.manager_id);
                },
                comment: "管理员表")
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "t_menu",
                columns: table => new
                {
                    menu_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    available = table.Column<short>(type: "smallint", nullable: false),
                    parent_id = table.Column<long>(type: "bigint", nullable: true),
                    path = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    level = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    title = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    icon_path = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, comment: "菜单图标", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_t_menu", x => x.menu_id);
                },
                comment: "菜单表")
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "t_role",
                columns: table => new
                {
                    role_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    role_name = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    available = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    remarks = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_t_role", x => x.role_id);
                },
                comment: "角色表")
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "t_user",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    email = table.Column<string>(type: "varchar(18)", maxLength: 18, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    identity = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    city = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sex = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phonenumber = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    create_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_t_user", x => x.user_id);
                },
                comment: "用户表")
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "t_car",
                columns: table => new
                {
                    car_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    registration = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    image = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    price = table.Column<float>(type: "float", nullable: false),
                    car_type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    color = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    brand = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    manager_id = table.Column<long>(type: "bigint", nullable: false),
                    status = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.car_id);
                    table.ForeignKey(
                        name: "fk_t_car_managers_manager_id",
                        column: x => x.manager_id,
                        principalTable: "t_manager",
                        principalColumn: "manager_id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "车辆表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_menu_role",
                columns: table => new
                {
                    menus_menu_id = table.Column<long>(type: "bigint", nullable: false),
                    role_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_t_menu_role", x => new { x.menus_menu_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_t_menu_role_t_menu_menus_menu_id",
                        column: x => x.menus_menu_id,
                        principalTable: "t_menu",
                        principalColumn: "menu_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_t_menu_role_t_role_role_id",
                        column: x => x.role_id,
                        principalTable: "t_role",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_role_manager",
                columns: table => new
                {
                    managers_manager_id = table.Column<long>(type: "bigint", nullable: false),
                    roles_role_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_t_role_manager", x => new { x.managers_manager_id, x.roles_role_id });
                    table.ForeignKey(
                        name: "fk_t_role_manager_t_manager_managers_manager_id",
                        column: x => x.managers_manager_id,
                        principalTable: "t_manager",
                        principalColumn: "manager_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_t_role_manager_t_role_roles_role_id",
                        column: x => x.roles_role_id,
                        principalTable: "t_role",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_booking",
                columns: table => new
                {
                    booking_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    booking_reference = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    content = table.Column<string>(type: "char(20)", maxLength: 20, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    price = table.Column<float>(type: "float", nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    car_id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_t_booking", x => x.booking_id);
                    table.ForeignKey(
                        name: "fk_t_booking_cars_car_id",
                        column: x => x.car_id,
                        principalTable: "t_car",
                        principalColumn: "car_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_t_booking_users_user_id",
                        column: x => x.user_id,
                        principalTable: "t_user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "订单表")
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "t_check",
                columns: table => new
                {
                    check_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    booking_id = table.Column<long>(type: "bigint", nullable: false),
                    check_reference = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    check_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    check_desc = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    pay_money = table.Column<float>(type: "float", nullable: false),
                    problem = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_t_check", x => x.check_id);
                    table.ForeignKey(
                        name: "fk_t_check_bookings_booking_id",
                        column: x => x.booking_id,
                        principalTable: "t_booking",
                        principalColumn: "booking_id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "检查单")
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateIndex(
                name: "ix_t_booking_car_id",
                table: "t_booking",
                column: "car_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_booking_user_id",
                table: "t_booking",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_car_manager_id",
                table: "t_car",
                column: "manager_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_check_booking_id",
                table: "t_check",
                column: "booking_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_t_menu_role_role_id",
                table: "t_menu_role",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_role_manager_roles_role_id",
                table: "t_role_manager",
                column: "roles_role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_check");

            migrationBuilder.DropTable(
                name: "t_log_login");

            migrationBuilder.DropTable(
                name: "t_menu_role");

            migrationBuilder.DropTable(
                name: "t_role_manager");

            migrationBuilder.DropTable(
                name: "t_booking");

            migrationBuilder.DropTable(
                name: "t_menu");

            migrationBuilder.DropTable(
                name: "t_role");

            migrationBuilder.DropTable(
                name: "t_car");

            migrationBuilder.DropTable(
                name: "t_user");

            migrationBuilder.DropTable(
                name: "t_manager");
        }
    }
}
