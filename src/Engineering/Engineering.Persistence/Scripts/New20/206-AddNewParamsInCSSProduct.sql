
BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ContractorStatusStatementProducts] ADD [CustomerInvoiceNumber] nvarchar(1500) NULL;
GO

ALTER TABLE [engineer].[ContractorStatusStatementProducts] ADD [DiscountOnInvoiceNumber] decimal(18,2) NULL;
GO

ALTER TABLE [engineer].[ContractorStatusStatementProducts] ADD [OtherPrice] decimal(18,2) NULL;
GO

ALTER TABLE [engineer].[ContractorStatusStatementProducts] ADD [Price] decimal(18,2) NULL;
GO

ALTER TABLE [engineer].[ContractorStatusStatementProducts] ADD [TransferPrice] decimal(18,2) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250512093918_AddNewParamsInCSSProduct', N'8.0.8');
GO

COMMIT;
GO

