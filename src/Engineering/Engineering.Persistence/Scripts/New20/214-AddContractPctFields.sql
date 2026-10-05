BEGIN TRANSACTION;
GO

EXEC sp_rename N'[engineer].[ContractorStatusStatementDetails].[PercentageOfFixContractTotalAmount]', N'ProjectFixedContractPct', N'COLUMN';
GO

ALTER TABLE [engineer].[ContractorStatusStatementDetails] ADD [FixedContractPct] decimal(5,2) NULL;
GO

ALTER TABLE [engineer].[ContractorStatusStatementDetails] ADD [FixedContractPctDesc] nvarchar(1500) NULL;
GO

ALTER TABLE [engineer].[ContractorStatusStatementDetails] ADD [ManagerFixedContractPct] decimal(5,2) NULL;
GO

ALTER TABLE [engineer].[ContractorStatusStatementDetails] ADD [ManagerFixedContractPctDesc] nvarchar(1500) NULL;
GO

ALTER TABLE [engineer].[ContractorStatusStatementDetails] ADD [ProjectFixedContractPctDesc] nvarchar(1500) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251012133252_AddContractPctFields', N'8.0.8');
GO

COMMIT;
GO



