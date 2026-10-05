BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260912054324_AddFinancialPeriodSynonym', N'8.0.8');
GO

COMMIT;
GO



