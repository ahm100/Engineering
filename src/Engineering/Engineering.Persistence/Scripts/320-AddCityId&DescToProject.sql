BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[Projects] ADD [CityId] bigint NULL;
DECLARE @description AS sql_variant;
SET @description = N'شناسه شهر';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Projects', 'COLUMN', N'CityId';
GO

ALTER TABLE [engineer].[Projects] ADD [Description] nvarchar(max) NULL;
DECLARE @description AS sql_variant;
SET @description = N'توضیحات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Projects', 'COLUMN', N'Description';
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ProjectOperationDependencies]') AND [c].[name] = N'LagDays');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ProjectOperationDependencies] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[ProjectOperationDependencies] ADD DEFAULT 0 FOR [LagDays];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260711104901_AddCityId&DescToProject', N'8.0.8');
GO

COMMIT;
GO



