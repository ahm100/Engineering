BEGIN TRANSACTION;
GO

DROP INDEX [IX_ProjectScheduleTasks_ProjectScheduleImportId_MppUid] ON [engineer].[ProjectScheduleTasks];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ProjectScheduleTasks]') AND [c].[name] = N'MppUid');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ProjectScheduleTasks] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[ProjectScheduleTasks] ALTER COLUMN [MppUid] int NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ProjectScheduleTasks]') AND [c].[name] = N'MppId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ProjectScheduleTasks] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [engineer].[ProjectScheduleTasks] ALTER COLUMN [MppId] int NULL;
GO

ALTER TABLE [engineer].[ProjectScheduleTasks] ADD [IsEstimated] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [engineer].[ProjectScheduleTasks] ADD [ParentId] bigint NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ProjectScheduleImports]') AND [c].[name] = N'FileName');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ProjectScheduleImports] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [engineer].[ProjectScheduleImports] ALTER COLUMN [FileName] nvarchar(250) NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ProjectScheduleImports]') AND [c].[name] = N'FileId');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ProjectScheduleImports] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [engineer].[ProjectScheduleImports] ALTER COLUMN [FileId] uniqueidentifier NULL;
GO

ALTER TABLE [engineer].[ProjectScheduleImports] ADD [ScheduleStartDate] datetime2 NULL;
GO

CREATE INDEX [IX_ProjectScheduleTasks_ParentId] ON [engineer].[ProjectScheduleTasks] ([ParentId]);
GO

CREATE UNIQUE INDEX [IX_ProjectScheduleTasks_ProjectScheduleImportId_MppUid] ON [engineer].[ProjectScheduleTasks] ([ProjectScheduleImportId], [MppUid]) WHERE [MppUid] IS NOT NULL;
GO

ALTER TABLE [engineer].[ProjectScheduleTasks] ADD CONSTRAINT [FK_ProjectScheduleTasks_ProjectScheduleTasks_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [engineer].[ProjectScheduleTasks] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260916103529_AddNewFieldsToProjectWbsAndTask', N'8.0.8');
GO

COMMIT;
GO



