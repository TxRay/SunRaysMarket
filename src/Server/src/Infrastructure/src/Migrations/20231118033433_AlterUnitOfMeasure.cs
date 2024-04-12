using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterUnitOfMeasure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Abbreviation",
                table: "UnitsOfMeasure",
                newName: "Symbol"
            );

            migrationBuilder.RenameIndex(
                name: "IX_UnitsOfMeasure_Abbreviation",
                table: "UnitsOfMeasure",
                newName: "IX_UnitsOfMeasure_Symbol"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Symbol",
                table: "UnitsOfMeasure",
                newName: "Abbreviation"
            );

            migrationBuilder.RenameIndex(
                name: "IX_UnitsOfMeasure_Symbol",
                table: "UnitsOfMeasure",
                newName: "IX_UnitsOfMeasure_Abbreviation"
            );
        }
    }
}
