BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[DailyProjectOperations] ADD [Type] int NOT NULL DEFAULT 1;
GO

ALTER TABLE [engineer].[DailyProjectOperationHistories] ADD [Type] int NOT NULL DEFAULT 1;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260808064657_AddDailyNewTypes', N'8.0.8');
GO

COMMIT;
GO



