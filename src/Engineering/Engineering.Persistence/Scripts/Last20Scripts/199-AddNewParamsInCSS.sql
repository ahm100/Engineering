BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ContractorStatusStatements] ADD [FinalManagerConfirmed] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [engineer].[ContractorStatusStatements] ADD [FinalManagerDescription] nvarchar(1500) NULL;
GO

ALTER TABLE [engineer].[ContractorStatusStatements] ADD [PrimaryManagerConfirmed] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [engineer].[ContractorStatusStatements] ADD [PrimaryManagerDescription] nvarchar(1500) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250503074704_AddNewParamsInCSS', N'8.0.8');
GO

COMMIT;
GO
