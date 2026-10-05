BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ContractorStatusStatements] ADD [CostOversAmount] decimal(18,2) NULL;
GO

ALTER TABLE [engineer].[ContractorStatusStatementHistories] ADD [CostOversAmount] decimal(18,2) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250509135121_AddCostOversAmount', N'8.0.8');
GO

COMMIT;
GO