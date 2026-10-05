BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[EmployerStatusStatementProjectOperations] ADD [TotalDailyWorkVolume] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

ALTER TABLE [engineer].[EmployerStatusStatementProjectOperations] ADD [TotalDetailWorkVolume] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

ALTER TABLE [engineer].[EmployerStatusStatementProjectOperationDetails] ADD [TotalDailyWorkVolume] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250421121627_NewChangesEntityESSParams', N'8.0.8');
GO

COMMIT;
GO
