using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Respository.Migrations
{
    /// <inheritdoc />
    public partial class thrs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .AlterColumn<string>(
                    name: "password",
                    table: "t_user",
                    type: "varchar(150)",
                    maxLength: 150,
                    nullable: false,
                    collation: "utf8mb4_general_ci",
                    oldClrType: typeof(string),
                    oldType: "varchar(18)",
                    oldMaxLength: 18
                )
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder
                .AlterColumn<string>(
                    name: "lname",
                    table: "t_user",
                    type: "varchar(15)",
                    maxLength: 15,
                    nullable: false,
                    collation: "utf8mb4_general_ci",
                    oldClrType: typeof(string),
                    oldType: "varchar(150)",
                    oldMaxLength: 150
                )
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .AlterColumn<string>(
                    name: "password",
                    table: "t_user",
                    type: "varchar(18)",
                    maxLength: 18,
                    nullable: false,
                    collation: "utf8mb4_general_ci",
                    oldClrType: typeof(string),
                    oldType: "varchar(150)",
                    oldMaxLength: 150
                )
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder
                .AlterColumn<string>(
                    name: "lname",
                    table: "t_user",
                    type: "varchar(150)",
                    maxLength: 150,
                    nullable: false,
                    collation: "utf8mb4_general_ci",
                    oldClrType: typeof(string),
                    oldType: "varchar(15)",
                    oldMaxLength: 15
                )
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");
        }
    }
}
