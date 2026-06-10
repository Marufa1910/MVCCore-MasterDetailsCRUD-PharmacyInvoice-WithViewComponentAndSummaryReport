# Pharmacy Invoice Management System (ASP.NET Core MVC)

A robust, enterprise-grade ASP.NET Core MVC web application designed to handle complex invoicing operations. This project showcases an advanced **Master-Details Form Architecture** for handling multi-item pharmaceutical invoice transactions, integrated with database-level performance aggregation using **SQL Stored Procedures**, dynamic asynchronous validation, and reusable user-interface view components.

## 🚀 Key Features

* **Master-Details Architecture:** Fully dynamic multi-item invoice creation (`Create.cshtml`) and editing (`Edit.cshtml`) managing parent tables (`Invoices`) and child records (`SaleItems`) concurrently.
* **Asynchronous Client Side Architecture:** Uses custom JavaScript hooks (`CreateNewInvoice()`, `UpdateInvoice()`, `AddItem()`) to build context payloads and manipulate forms dynamically.
* **Encapsulated UI Components:** Implements a decoupled `@await Component.InvokeAsync("HeadCount")` View Component to stream metadata summaries seamlessly into the Index view wrapper.
* **Live Preview File Upload:** Rich UX layout supporting reactive profile/document uploads with instantaneous image source previews via client-side base-64 processing (`readUrl`).
* **SQL Server Aggregation Reports:** A dynamic data-driven executive summary report built using data aggregation metrics ($\min$, $\max$, $\sum$, $\text{avg}$, and frequency count) filtered globally and broken down via `GroupBy` operational dimensions.

---

## 🛠️ Tech Stack & Architecture

* **Framework:** .NET Core (MVC Pattern)
* **Database:** Microsoft SQL Server
* **Frontend:** Razor Pages Engine, Bootstrap 5, Vanilla JavaScript Engine / jQuery

---

## 🗄️ Database Setup & Stored Procedures

Before running the application, you must initialize the database schema and populate the required relational stored procedures.

1. Open **SQL Server Management Studio (SSMS)** and connect to your instance.
2. Create a new database named `InvoiceDb`.
3. Locate the initialization script included in this repository under `/Database/DbScript.sql`.
4. Open the script in SSMS and execute it against your `InvoiceDb` context.

### Required Stored Procedure Summary Structure
The reporting dashboard expects a procedure processing statistical summaries similar to the logic block below:
```sql
CREATE PROCEDURE sp_GetInvoiceExecutiveSummary
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Overall Stats and Aggregated Grouping logic across Transaction Types
    SELECT 
        TransactionTypeName,
        MIN(UnitPrice) AS MinValue,
        MAX(UnitPrice) AS MaxValue,
        SUM(UnitPrice * Quantity) AS SumValue,
        AVG(UnitPrice) AS AvgValue,
        COUNT(*) AS Count
    FROM SaleItems s
    JOIN Invoices i ON s.InvoiceId = i.InvoiceId
    JOIN TransactionTypes t ON i.TransactionTypeId = t.TransactionTypeId
    GROUP BY TransactionTypeName;
END
