BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[Projects]') AND [c].[name] = N'ProjectTypesId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[Projects] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[Projects] ALTER COLUMN [ProjectTypesId] bigint NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[Projects]') AND [c].[name] = N'ProjectTypeId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[Projects] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [engineer].[Projects] ALTER COLUMN [ProjectTypeId] bigint NULL;
GO

ALTER TABLE [engineer].[Projects] ADD [IsOrganizationUnit] bit NOT NULL DEFAULT CAST(0 AS bit);
DECLARE @description AS sql_variant;
SET @description = N'واحد سازمانی هست یا نه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Projects', 'COLUMN', N'IsOrganizationUnit';
GO

ALTER TABLE [engineer].[Projects] ADD [OrganizationId] bigint NULL;
DECLARE @description AS sql_variant;
SET @description = N'شناسه سازمان';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Projects', 'COLUMN', N'OrganizationId';
GO

CREATE INDEX [IX_Projects_OrganizationId] ON [engineer].[Projects] ([OrganizationId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260715051556_HandleOrganizationInProject', N'8.0.8');
GO

COMMIT;
GO



