BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ContractorContractDetails] ADD [ContractCoefficient] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260726112921_AddContractCoefficient', N'8.0.8');
GO

COMMIT;
GO



