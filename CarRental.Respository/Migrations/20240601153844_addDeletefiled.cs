using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Respository.Migrations
{
    /// <inheritdoc />
    public partial class addDeletefiled : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_t_car_managers_manager_id",
                table: "t_car");

            migrationBuilder.AddColumn<byte>(
                name: "deleted",
                table: "t_role",
                type: "tinyint unsigned",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "is_deleted",
                table: "t_menu",
                type: "tinyint unsigned",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddForeignKey(
                name: "fk_t_car_t_manager_manager_id",
                table: "t_car",
                column: "manager_id",
                principalTable: "t_manager",
                principalColumn: "manager_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_t_car_t_manager_manager_id",
                table: "t_car");

            migrationBuilder.DropColumn(
                name: "deleted",
                table: "t_role");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "t_menu");

            migrationBuilder.AddForeignKey(
                name: "fk_t_car_managers_manager_id",
                table: "t_car",
                column: "manager_id",
                principalTable: "t_manager",
                principalColumn: "manager_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
