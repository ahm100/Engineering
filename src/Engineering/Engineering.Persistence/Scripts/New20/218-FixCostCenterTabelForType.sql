BEGIN TRANSACTION;
GO


INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251026060308_FixCostCenterTabelForType', N'8.0.8');
GO

COMMIT;
GO



