BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ContractorStatusStatementProducts] ADD [TaxNumber] decimal(18,2) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251109082900_AddTotalPriceINProductCSS', N'8.0.8');
GO

COMMIT;
GO



