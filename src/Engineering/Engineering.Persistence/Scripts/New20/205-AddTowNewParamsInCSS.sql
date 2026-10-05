BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ContractorStatusStatements] ADD [CanPayableAmount] decimal(18,2) NULL;
GO

ALTER TABLE [engineer].[ContractorStatusStatements] ADD [DiscountPrice] decimal(18,2) NULL;
GO

ALTER TABLE [engineer].[ContractorStatusStatements] ADD [PayableAmount] decimal(18,2) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250511235642_AddTowNewParamsInCSS', N'8.0.8');
GO

COMMIT;
GO