BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251026062958_LastMigs', N'8.0.8');
GO

COMMIT;
GO



