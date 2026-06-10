using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _1293481Evidence.Migrations
{
    /// <inheritdoc />
    public partial class sp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sys.types WHERE name = 'ParamSaleItemType' AND is_table_type = 1)
                BEGIN
                    CREATE TYPE dbo.ParamSaleItemType AS TABLE(
                        MedicineName VARCHAR(50),
                        Quantity INT,
                        UnitPrice DECIMAL(18, 2)
                    );
                END");

            migrationBuilder.Sql(@"
                CREATE OR ALTER PROCEDURE dbo.InsertInvoiceSP
                    @InvoiceNo VARCHAR(50),
                    @TransactionDate DATETIME,
                    @ClientName VARCHAR(50),
                    @IsNewClient BIT,
                    @ReferrerName VARCHAR(50),
                    @TransactionTypeId INT,
                    @ImageUrl VARCHAR(MAX),
                    @SaleItems dbo.ParamSaleItemType READONLY
                AS
                BEGIN
                    SET NOCOUNT ON;
                    BEGIN TRY
                        BEGIN TRANSACTION;

                        DECLARE @InvoiceId INT;

                        INSERT INTO dbo.Invoice
                            (InvoiceNo, TransactionDate, ClientName, IsNewClient, ReferrerName, TransactionTypeId, ImageUrl)
                        VALUES
                            (@InvoiceNo, @TransactionDate, @ClientName, @IsNewClient, @ReferrerName, @TransactionTypeId, @ImageUrl);

                        SET @InvoiceId = SCOPE_IDENTITY();

                        INSERT INTO dbo.SaleItem (MedicineName, Quantity, UnitPrice, InvoiceId)
                        SELECT MedicineName, Quantity, UnitPrice, @InvoiceId
                        FROM @SaleItems;

                        COMMIT TRANSACTION;
                    END TRY
                    BEGIN CATCH
                        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;

                        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
                        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
                        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
                    END CATCH
                END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS dbo.InsertInvoiceSP");
            migrationBuilder.Sql(@"DROP TYPE IF EXISTS dbo.ParamSaleItemType");
        }
    }
}
