using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ApplyUpdatesBeforeSeedingStores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Stores_Address_Id", table: "Stores");

            migrationBuilder.RenameColumn(
                name: "UnitOfMeasurement",
                table: "OrderLine",
                newName: "UnitPrice"
            );

            migrationBuilder.AddColumn<int>(
                name: "BillingAddressId",
                table: "Transactions",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder
                .AlterColumn<int>(
                    name: "Id",
                    table: "Stores",
                    type: "integer",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "integer"
                )
                .Annotation(
                    "Npgsql:ValueGenerationStrategy",
                    NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                );

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Stores",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BillingAddressId",
                table: "Transactions",
                column: "BillingAddressId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Stores_AddressId",
                table: "Stores",
                column: "AddressId",
                unique: true
            );

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Stores_Address_AddressId", table: "Stores");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Address_BillingAddressId",
                table: "Transactions"
            );

            migrationBuilder.DropIndex(
                name: "IX_Transactions_BillingAddressId",
                table: "Transactions"
            );

            migrationBuilder.DropIndex(name: "IX_Stores_AddressId", table: "Stores");

            migrationBuilder.DropColumn(name: "BillingAddressId", table: "Transactions");

            migrationBuilder.DropColumn(name: "AddressId", table: "Stores");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "OrderLine",
                newName: "UnitOfMeasurement"
            );

            migrationBuilder
                .AlterColumn<int>(
                    name: "Id",
                    table: "Stores",
                    type: "integer",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "integer"
                )
                .OldAnnotation(
                    "Npgsql:ValueGenerationStrategy",
                    NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                );

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Address_Id",
                table: "Stores",
                column: "Id",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict
            );
        }
    }
}
