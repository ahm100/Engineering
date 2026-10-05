BEGIN TRANSACTION;
GO

EXEC sp_rename N'[engineer].[EmployerStatusStatements].[SendStatusType]', N'Status', N'COLUMN';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250427113928_ReNamePropESS', N'8.0.8');
GO

COMMIT;
GO