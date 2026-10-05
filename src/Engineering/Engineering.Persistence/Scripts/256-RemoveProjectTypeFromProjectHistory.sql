BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ProjectHistories] DROP CONSTRAINT [FK_ProjectHistories_EngineeringProjectTypes_ProjectTypesId];
GO

DROP INDEX [IX_ProjectHistories_ProjectTypesId] ON [engineer].[ProjectHistories];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ProjectHistories]') AND [c].[name] = N'ProjectTypesId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ProjectHistories] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[ProjectHistories] DROP COLUMN [ProjectTypesId];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ProjectHistories]') AND [c].[name] = N'ProjectTypeId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ProjectHistories] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [engineer].[ProjectHistories] ALTER COLUMN [ProjectTypeId] bigint NULL;
GO

CREATE INDEX [IX_ProjectHistories_ProjectTypeId] ON [engineer].[ProjectHistories] ([ProjectTypeId]);
GO

ALTER TABLE [engineer].[ProjectHistories] ADD CONSTRAINT [FK_ProjectHistories_EngineeringProjectTypes_ProjectTypeId] FOREIGN KEY ([ProjectTypeId]) REFERENCES [engineer].[EngineeringProjectTypes] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251207113450_RemoveProjectTypeFromProjectHistory', N'8.0.8');
GO

COMMIT;
GO



