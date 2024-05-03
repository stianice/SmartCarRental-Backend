using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Respository.Migrations
{
    /// <inheritdoc />
    public partial class x3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .AddColumn<string>(
                    name: "IconPath",
                    table: "t_menu",
                    type: "varchar(20)",
                    maxLength: 20,
                    nullable: false,
                    defaultValue: "",
                    comment: "菜单图标",
                    collation: "utf8mb4_general_ci"
                )
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "IconPath", table: "t_menu");
        }
    }
}
