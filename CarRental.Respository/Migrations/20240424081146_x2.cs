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
            migrationBuilder.DropColumn(name: "Available", table: "t_role");

            migrationBuilder
                .AddColumn<string>(
                    name: "Remarks",
                    table: "t_role",
                    type: "longtext",
                    nullable: false,
                    collation: "utf8mb4_general_ci"
                )
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Remarks", table: "t_role");

            migrationBuilder.AddColumn<byte>(
                name: "Available",
                table: "t_role",
                type: "tinyint unsigned",
                nullable: false,
                defaultValue: (byte)0
            );
        }
    }
}
