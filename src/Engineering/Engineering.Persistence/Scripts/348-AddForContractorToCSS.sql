BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ContractorStatusStatements] ADD [ForContractorProductsAmount] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

ALTER TABLE [engineer].[ContractorStatusStatementProducts] ADD [IsPurchaseForContractor] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260815074322_AddForContractorToCSS', N'8.0.8');
GO

COMMIT;
GO



