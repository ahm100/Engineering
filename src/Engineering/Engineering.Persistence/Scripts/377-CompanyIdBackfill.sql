BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260902045919_CompanyIdBackfill', N'8.0.8');
GO

COMMIT;
GO



