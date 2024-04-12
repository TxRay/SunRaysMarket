using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateCustomerAddressRelationshipAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAddress_Addresses_AddressId",
                table: "CustomerAddress"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAddress_Customers_CustomerId",
                table: "CustomerAddress"
            );

            migrationBuilder.DropPrimaryKey(name: "PK_CustomerAddress", table: "CustomerAddress");

            migrationBuilder.RenameTable(name: "CustomerAddress", newName: "CustomerAddresses");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerAddress_CustomerId_AddressId",
                table: "CustomerAddresses",
                newName: "IX_CustomerAddresses_CustomerId_AddressId"
            );

            migrationBuilder.RenameIndex(
                name: "IX_CustomerAddress_AddressId",
                table: "CustomerAddresses",
                newName: "IX_CustomerAddresses_AddressId"
            );

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerAddresses",
                table: "CustomerAddresses",
                column: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAddresses_Addresses_AddressId",
                table: "CustomerAddresses",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAddresses_Customers_CustomerId",
                table: "CustomerAddresses",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAddresses_Addresses_AddressId",
                table: "CustomerAddresses"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAddresses_Customers_CustomerId",
                table: "CustomerAddresses"
            );

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerAddresses",
                table: "CustomerAddresses"
            );

            migrationBuilder.RenameTable(name: "CustomerAddresses", newName: "CustomerAddress");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerAddresses_CustomerId_AddressId",
                table: "CustomerAddress",
                newName: "IX_CustomerAddress_CustomerId_AddressId"
            );

            migrationBuilder.RenameIndex(
                name: "IX_CustomerAddresses_AddressId",
                table: "CustomerAddress",
                newName: "IX_CustomerAddress_AddressId"
            );

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerAddress",
                table: "CustomerAddress",
                column: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAddress_Addresses_AddressId",
                table: "CustomerAddress",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAddress_Customers_CustomerId",
                table: "CustomerAddress",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }
    }
}
