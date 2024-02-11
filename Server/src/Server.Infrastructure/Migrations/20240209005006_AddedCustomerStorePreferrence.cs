using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedCustomerStorePreferrence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PreferredStoreId",
                table: "Customers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PreferredStoreId",
                table: "Customers",
                column: "PreferredStoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Stores_PreferredStoreId",
                table: "Customers",
                column: "PreferredStoreId",
                principalTable: "Stores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Stores_PreferredStoreId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_PreferredStoreId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PreferredStoreId",
                table: "Customers");
        }
    }
}
