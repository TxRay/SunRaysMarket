using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDeliveryAddressToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Stores_Address_AddressId", table: "Stores");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Address_BillingAddressId",
                table: "Transactions"
            );

            migrationBuilder.DropForeignKey(name: "FK_Users_Address_AddressId", table: "Users");

            migrationBuilder.DropPrimaryKey(name: "PK_Address", table: "Address");

            migrationBuilder.RenameTable(name: "Address", newName: "Addresses");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryAddressId",
                table: "Orders",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AddPrimaryKey(name: "PK_Addresses", table: "Addresses", column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryAddressId",
                table: "Orders",
                column: "DeliveryAddressId"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Addresses_DeliveryAddressId",
                table: "Orders",
                column: "DeliveryAddressId",
                principalTable: "Addresses",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Addresses_AddressId",
                table: "Stores",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Addresses_BillingAddressId",
                table: "Transactions",
                column: "BillingAddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Addresses_AddressId",
                table: "Users",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Addresses_DeliveryAddressId",
                table: "Orders"
            );

            migrationBuilder.DropForeignKey(name: "FK_Stores_Addresses_AddressId", table: "Stores");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Addresses_BillingAddressId",
                table: "Transactions"
            );

            migrationBuilder.DropForeignKey(name: "FK_Users_Addresses_AddressId", table: "Users");

            migrationBuilder.DropIndex(name: "IX_Orders_DeliveryAddressId", table: "Orders");

            migrationBuilder.DropPrimaryKey(name: "PK_Addresses", table: "Addresses");

            migrationBuilder.DropColumn(name: "DeliveryAddressId", table: "Orders");

            migrationBuilder.RenameTable(name: "Addresses", newName: "Address");

            migrationBuilder.AddPrimaryKey(name: "PK_Address", table: "Address", column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Address_AddressId",
                table: "Stores",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Address_BillingAddressId",
                table: "Transactions",
                column: "BillingAddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Address_AddressId",
                table: "Users",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id"
            );
        }
    }
}
