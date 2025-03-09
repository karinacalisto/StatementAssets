using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Statement.Data.Migrations
{
    /// <inheritdoc />
    public partial class NomeDaMigracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "statement");

            migrationBuilder.CreateTable(
                name: "accounts",
                schema: "statement",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    agency = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    accountnumber = table.Column<string>(type: "text", nullable: false),
                    dac = table.Column<int>(type: "integer", nullable: false),
                    accountname = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    isdraft = table.Column<bool>(type: "boolean", nullable: false),
                    data = table.Column<string>(type: "JSONB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cash_composition",
                schema: "statement",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    productname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    productcode = table.Column<int>(type: "integer", nullable: false),
                    investedbalance = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cash_composition", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "investments",
                schema: "statement",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    productid = table.Column<Guid>(type: "uuid", nullable: false),
                    accountid = table.Column<Guid>(type: "uuid", nullable: false),
                    investedbalance = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    investedat = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    productname = table.Column<string>(type: "text", nullable: false),
                    productcode = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_investments", x => x.id);
                    table.ForeignKey(
                        name: "FK_investments_accounts_accountid",
                        column: x => x.accountid,
                        principalSchema: "statement",
                        principalTable: "accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_investments_cash_composition_productid",
                        column: x => x.productid,
                        principalSchema: "statement",
                        principalTable: "cash_composition",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_investments_accountid",
                schema: "statement",
                table: "investments",
                column: "accountid");

            migrationBuilder.CreateIndex(
                name: "IX_investments_productid",
                schema: "statement",
                table: "investments",
                column: "productid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "investments",
                schema: "statement");

            migrationBuilder.DropTable(
                name: "accounts",
                schema: "statement");

            migrationBuilder.DropTable(
                name: "cash_composition",
                schema: "statement");
        }
    }
}
