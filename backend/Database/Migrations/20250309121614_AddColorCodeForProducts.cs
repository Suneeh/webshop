using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddColorCodeForProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ColorCodeHex",
                table: "Products",
                type: "character varying(6)",
                maxLength: 6,
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql("""UPDATE public."Products" SET "ColorCodeHex" = LEFT("Description", 6);""");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorCodeHex",
                table: "Products");
        }
    }
}
