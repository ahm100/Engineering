BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ContractorStatusStatements] ADD [FinalManagerConfirmedAmount] decimal(18,2) NULL;
GO

ALTER TABLE [engineer].[ContractorStatusStatements] ADD [PrimaryManagerConfirmedAmount] decimal(18,2) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250423122128_AddNewContractorStatusStatementAmounts', N'8.0.8');
GO

COMMIT;
GO
