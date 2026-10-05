BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ContractorStatusStatementDetails] ADD [PercentageOfFixContractTotalAmount] decimal(5,2) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250521081653_AddnewPopInCSS', N'8.0.8');
GO

COMMIT;
GO
