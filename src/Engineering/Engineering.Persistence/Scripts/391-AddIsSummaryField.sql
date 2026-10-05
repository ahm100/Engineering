BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ProjectScheduleTasks] ADD [IsSummary] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260919074417_AddIsSummaryField', N'8.0.8');
GO

COMMIT;
GO



