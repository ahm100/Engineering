BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ContractorContractDetails] ADD [TotalCostOveredAmount] decimal(18,2) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250430064506_AddTotalCostOveredAmount', N'8.0.8');
GO

COMMIT;
GO
