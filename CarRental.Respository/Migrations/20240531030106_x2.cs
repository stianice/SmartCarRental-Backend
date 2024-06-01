using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Respository.Migrations
{
    /// <inheritdoc />
    public partial class x2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_t_menu_parent_id",
                table: "t_menu",
                column: "parent_id");

            migrationBuilder.AddForeignKey(
                name: "fk_t_menu_t_menu_parent_id",
                table: "t_menu",
                column: "parent_id",
                principalTable: "t_menu",
                principalColumn: "menu_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_t_menu_t_menu_parent_id",
                table: "t_menu");

            migrationBuilder.DropIndex(
                name: "ix_t_menu_parent_id",
                table: "t_menu");
        }
    }
}
