using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _1293481Evidence.Migrations
{
    /// <inheritdoc />
    public partial class deleteItemSp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE dbo.DeleteInvoiceItemSp
                    @InvoiceId INT
                AS
                BEGIN
                    SET NOCOUNT ON;

                    -- Start a Transaction to ensure both deletes happen or none do
                    BEGIN TRANSACTION;

                    BEGIN TRY
                       
                        DELETE FROM SaleItem WHERE InvoiceId = @InvoiceId;

                       
                        DELETE FROM Invoice WHERE InvoiceId = @InvoiceId;

                        COMMIT TRANSACTION;
                    END TRY
                    BEGIN CATCH
                        ROLLBACK TRANSACTION;
                        RAISERROR('Error occurred while deleting the invoice and its items.', 16, 1);
                    END CATCH
                END
            ");
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS dbo.DeleteInvoiceItemSp");
        }
    }
}
