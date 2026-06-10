using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _1293481Evidence.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "TransactionType",
            //    columns: table => new
            //    {
            //        TransactionTypeId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        TransactionTypeName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Transact__20266D0BDAFDD8AC", x => x.TransactionTypeId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Invoice",
            //    columns: table => new
            //    {
            //        InvoiceId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        InvoiceNo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
            //        TransactionDate = table.Column<DateTime>(type: "datetime", nullable: false),
            //        ClientName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
            //        IsNewClient = table.Column<bool>(type: "bit", nullable: false),
            //        ReferrerName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
            //        TransactionTypeId = table.Column<int>(type: "int", nullable: false),
            //        ImageUrl = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Invoice__D796AAB5E93BEE1D", x => x.InvoiceId);
            //        table.ForeignKey(
            //            name: "FK__Invoice__Transac__4BAC3F29",
            //            column: x => x.TransactionTypeId,
            //            principalTable: "TransactionType",
            //            principalColumn: "TransactionTypeId",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "SaleItem",
            //    columns: table => new
            //    {
            //        SaleItemId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        MedicineName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
            //        Quantity = table.Column<int>(type: "int", nullable: false),
            //        UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        InvoiceId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__SaleItem__C6059401575C490A", x => x.SaleItemId);
            //        table.ForeignKey(
            //            name: "FK__SaleItem__Invoic__4E88ABD4",
            //            column: x => x.InvoiceId,
            //            principalTable: "Invoice",
            //            principalColumn: "InvoiceId",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.InsertData(
            //    table: "TransactionType",
            //    columns: new[] { "TransactionTypeId", "TransactionTypeName" },
            //    values: new object[,]
            //    {
            //        { 1, "WholeSale" },
            //        { 2, "Retail" },
            //        { 3, "Internal Sale" }
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Invoice_TransactionTypeId",
            //    table: "Invoice",
            //    column: "TransactionTypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SaleItem_InvoiceId",
            //    table: "SaleItem",
            //    column: "InvoiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "SaleItem");

            //migrationBuilder.DropTable(
            //    name: "Invoice");

            //migrationBuilder.DropTable(
            //    name: "TransactionType");
        }
    }
}
