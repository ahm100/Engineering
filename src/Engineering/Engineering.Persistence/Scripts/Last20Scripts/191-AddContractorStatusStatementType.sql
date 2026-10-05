BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ContractorStatusStatements] ADD [Type] int NOT NULL DEFAULT 1;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250422114423_AddContractorStatusStatementType', N'8.0.8');
GO

COMMIT;
GO
