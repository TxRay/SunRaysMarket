using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderAndTransactionNumbers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Code", table: "Transactions");

            migrationBuilder.CreateSequence(name: "OrderNumbers", startValue: 1000000000L);

            migrationBuilder.CreateSequence(name: "TransactionNumbers", startValue: 1000000000L);

            migrationBuilder.AddColumn<long>(
                name: "TransactionNumber",
                table: "Transactions",
                type: "bigint",
                nullable: false,
                defaultValueSql: "nextval('\"TransactionNumbers\"')"
            );

            migrationBuilder.AddColumn<long>(
                name: "OrderNumber",
                table: "Orders",
                type: "bigint",
                nullable: false,
                defaultValueSql: "nextval('\"OrderNumbers\"')"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "TransactionNumber", table: "Transactions");

            migrationBuilder.DropColumn(name: "OrderNumber", table: "Orders");

            migrationBuilder.DropSequence(name: "OrderNumbers");

            migrationBuilder.DropSequence(name: "TransactionNumbers");

            migrationBuilder.AddColumn<int>(
                name: "Code",
                table: "Transactions",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );
        }
    }
}
