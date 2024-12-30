using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TransactionsIndexCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "postgres");

            migrationBuilder.CreateTable(
                name: "transactions",
                schema: "postgres",
                columns: table => new
                {
                    transaction_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    category = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    merchant = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactions", x => x.transaction_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_Category_Count",
                schema: "postgres",
                table: "transactions",
                column: "category")
                .Annotation("Npgsql:IndexInclude", new[] { "transaction_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId_Amount_Expense",
                schema: "postgres",
                table: "transactions",
                columns: new[] { "user_id", "amount" },
                filter: "amount < 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transactions",
                schema: "postgres");
        }
    }
}
