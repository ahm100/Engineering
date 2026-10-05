BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ContractorStatusStatementPayments] ADD [PaymentedAmount] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251111181809_FixingMigs', N'8.0.8');
GO

COMMIT;
GO



