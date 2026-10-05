BEGIN TRANSACTION;
GO

DROP INDEX [IX_Projects_OrganizationId] ON [engineer].[Projects];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[Projects]') AND [c].[name] = N'EmployerId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[Projects] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[Projects] ALTER COLUMN [EmployerId] bigint NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ProjectHistories]') AND [c].[name] = N'EmployerId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ProjectHistories] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [engineer].[ProjectHistories] ALTER COLUMN [EmployerId] bigint NULL;
GO

ALTER TABLE [engineer].[ProjectHistories] ADD [IsOrganizationUnit] bit NOT NULL DEFAULT CAST(0 AS bit);
DECLARE @description AS sql_variant;
SET @description = N'واحد سازمانی هست یا نه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectHistories', 'COLUMN', N'IsOrganizationUnit';
GO

ALTER TABLE [engineer].[ProjectHistories] ADD [OrganizationId] bigint NULL;
DECLARE @description AS sql_variant;
SET @description = N'شناسه سازمان';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectHistories', 'COLUMN', N'OrganizationId';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260715080537_MakeEmployerIdNullable', N'8.0.8');
GO

COMMIT;
GO



