using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBillingAddressColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Addresses_BillingAddressId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_BillingAddressId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "BillingAddressId",
                table: "Transactions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BillingAddressId",
                table: "Transactions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BillingAddressId",
                table: "Transactions",
                column: "BillingAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Addresses_BillingAddressId",
                table: "Transactions",
                column: "BillingAddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
