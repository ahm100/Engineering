BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ProjectWbses] DROP CONSTRAINT [FK_ProjectWbses_Projects_ProjectId];
GO

ALTER TABLE [engineer].[ProjectWbses] DROP CONSTRAINT [FK_ProjectWbses_WbsTemplates_WbsTemplateId];
GO

ALTER TABLE [engineer].[ProjectOperationHistories] ADD [ActualFinishDate] datetime2 NULL;
GO

ALTER TABLE [engineer].[ProjectOperationHistories] ADD [ActualStartDate] datetime2 NULL;
GO

ALTER TABLE [engineer].[ProjectOperationHistories] ADD [BaselineDuration] int NULL;
GO

ALTER TABLE [engineer].[ProjectOperationHistories] ADD [BaselineFinishDate] datetime2 NULL;
GO

ALTER TABLE [engineer].[ProjectOperationHistories] ADD [BaselineStartDate] datetime2 NULL;
GO

ALTER TABLE [engineer].[ProjectOperationHistories] ADD [PlannedDuration] int NULL;
GO

ALTER TABLE [engineer].[ProjectOperationHistories] ADD [PlannedFinishDate] datetime2 NULL;
GO

ALTER TABLE [engineer].[ProjectOperationHistories] ADD [PlannedStartDate] datetime2 NULL;
GO

ALTER TABLE [engineer].[ProjectWbses] ADD CONSTRAINT [FK_ProjectWbses_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [engineer].[Projects] ([Id]);
GO

ALTER TABLE [engineer].[ProjectWbses] ADD CONSTRAINT [FK_ProjectWbses_WbsTemplates_WbsTemplateId] FOREIGN KEY ([WbsTemplateId]) REFERENCES [engineer].[WbsTemplates] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260623114838_AddNewParamsToPOHistory', N'8.0.8');
GO

COMMIT;
GO



