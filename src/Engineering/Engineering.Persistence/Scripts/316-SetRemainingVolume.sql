BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260630075241_SetRemainingVolume', N'8.0.8');
GO

COMMIT;
GO



