using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _1293481Evidence.Migrations
{
    /// <inheritdoc />
    public partial class edit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE OR ALTER PROCEDURE dbo.UpdateInvoiceSP
                @InvoiceId INT,
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

                   
                    UPDATE dbo.Invoice
                    SET InvoiceNo = @InvoiceNo,
                        TransactionDate = @TransactionDate,
                        ClientName = @ClientName,
                        IsNewClient = @IsNewClient,
                        ReferrerName = @ReferrerName,
                        TransactionTypeId = @TransactionTypeId,
                        ImageUrl = @ImageUrl
                    WHERE InvoiceId = @InvoiceId;

                   
                    DELETE FROM dbo.SaleItem WHERE InvoiceId = @InvoiceId;

                    
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS dbo.UpdateInvoiceSP");
        }
    }
}
