BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251022060644_RefactorCostCenter', N'8.0.8');
GO

COMMIT;
GO



