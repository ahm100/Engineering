BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251026065144_ModifyLastMigs', N'8.0.8');
GO

COMMIT;
GO



