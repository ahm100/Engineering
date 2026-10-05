BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ContractorContracts] ADD [DailyBaseHours] decimal(18,2) NULL;
GO

ALTER TABLE [engineer].[ContractorContracts] ADD [MonthlyBaseHours] decimal(18,2) NULL;
GO

ALTER TABLE [engineer].[ContractorContractHistories] ADD [DailyBaseHours] decimal(18,2) NULL;
GO

ALTER TABLE [engineer].[ContractorContractHistories] ADD [MonthlyBaseHours] decimal(18,2) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260701084217_AddDaily&MonthlyBaseHours', N'8.0.8');
GO

COMMIT;
GO



