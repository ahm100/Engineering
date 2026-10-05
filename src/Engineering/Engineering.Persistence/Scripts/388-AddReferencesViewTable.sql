BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260913081731_AddReferencesViewTable', N'8.0.8');
GO

COMMIT;
GO



