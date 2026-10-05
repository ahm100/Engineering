BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ProjectScheduleTasks] ADD [IsManuallyScheduled] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260913121432_AddManualFieldToProjectScheduleTask', N'8.0.8');
GO

COMMIT;
GO



