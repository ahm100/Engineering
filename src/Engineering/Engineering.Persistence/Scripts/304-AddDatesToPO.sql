BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ProjectOperations] ADD [ActualFinishDate] datetime2 NULL;
GO

ALTER TABLE [engineer].[ProjectOperations] ADD [ActualStartDate] datetime2 NULL;
GO

ALTER TABLE [engineer].[ProjectOperations] ADD [BaselineDuration] int NULL;
GO

ALTER TABLE [engineer].[ProjectOperations] ADD [BaselineFinishDate] datetime2 NULL;
GO

ALTER TABLE [engineer].[ProjectOperations] ADD [BaselineStartDate] datetime2 NULL;
GO

ALTER TABLE [engineer].[ProjectOperations] ADD [PlannedDuration] int NULL;
GO

ALTER TABLE [engineer].[ProjectOperations] ADD [PlannedFinishDate] datetime2 NULL;
GO

ALTER TABLE [engineer].[ProjectOperations] ADD [PlannedStartDate] datetime2 NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260616125432_AddDatesToPO', N'8.0.8');
GO

COMMIT;
GO



